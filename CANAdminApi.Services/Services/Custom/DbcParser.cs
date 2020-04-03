using CANAdminApi.Services.BMOModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CANAdminApi.Services.Services.Custom
{
    public class DbcParser
    {
        // BU_ = Network Node = Name only (BU_: GearBox EngineTest EngineControl DashBoard ABS) (GearBox)
        // BO_ = CAN Message = ID, Name (BO_ 199 WheelInfoIEEE: 8 ABS) (199, WheelInfoIEEE)
        // SG_ = CAN Signal = Name, Start bit, length (SG_ WheelSpeedFR : 32|32@1- (1,0) [0|1300] "1/min"  GearBox,EngineControl) (WheelSpeedFR, 32,32)
        // EV_ = Environment Variable
        private List<string> LIST = new List<string>();

        public DbcParser() 
        { 
            
        }

        public async System.Threading.Tasks.Task<FileBMO> LoadFromStreamAsync(string fileName, Stream stream)
        {
           // stream.Seek(0, SeekOrigin.Begin);

            FileBMO obj = new FileBMO();
            obj.FileName = fileName;
            obj.MimeType = MimeMapping.MimeUtility.GetMimeMapping(fileName);
            obj.Extension = Path.GetExtension(fileName);
            obj.FileContent = await ReadFullyAsync(stream);
            using(var ms = new MemoryStream(obj.FileContent))
            using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
            {
                await ReadLinesAsync(reader); 
            }
          
            obj.NetworkNodes = GetNetworkNodes();
            return obj;
        }

        private async System.Threading.Tasks.Task<byte[]> ReadFullyAsync(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await input.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        private async System.Threading.Tasks.Task ReadLinesAsync(StreamReader content)
        {
            string line; 

            while ((line = await content.ReadLineAsync().ConfigureAwait(false)) != null)
            { 
                LIST.Add(line);
            }
            if (LIST.Count == 0) throw new Exception("File is empty!");
        }

        private IEnumerable<NetworkNodeBMO> GetNetworkNodes()
        {
            List<NetworkNodeBMO> list = new List<NetworkNodeBMO>();
            foreach(var line in LIST)
            {
                if(line.StartsWith("BU_: "))
                {
                    var networkNodes = line.Split(' ').ToList(); 
                    if (networkNodes.Count > 0) networkNodes.RemoveAt(0); // remove the "BU_:"// start from index >0
                    if (networkNodes.Count == 0) throw new Exception("File does not contain Network Node names.");
                    list.AddRange(networkNodes.Select(a => new NetworkNodeBMO() { Name = a }).ToList());

                    list.ForEach(a => a.CanMessages = GetCanMessages(a));
                   
                }
            }

            return list;
        }

        private IEnumerable<CanMessageBMO> GetCanMessages(NetworkNodeBMO networkNode)
        { 
            List<CanMessageBMO> list = new List<CanMessageBMO>();
            int i = 0;
            foreach (var line in LIST)
            {
                if (line.StartsWith("BO_ "))
                {
                    var messages = line.Split(' ').ToList();  
                    if (messages.Count > 0) messages.RemoveAt(0); 
                    if (messages.Count == 0) throw new Exception("File does not contain Can Messages.");

                    CanMessageBMO message = new CanMessageBMO();
                    message.ID = long.Parse(messages[0]);
                    message.Name = messages[1]?.Replace(":", "");

                    message.CanSignals = GetCanSignals(networkNode, i);
                    // should we ignore this if there is no signals (as a side effect, of not having relations to any network node)?
                    list.Add(message);
                }
                i++;
            }

            return list;
        }

        private IEnumerable<CanSignalBMO> GetCanSignals(NetworkNodeBMO networkNode, int boLineIndex)
        {
            List<CanSignalBMO> list = new List<CanSignalBMO>();
            for(var i = boLineIndex + 1; i < LIST.Count; i++)
            {
                if (string.IsNullOrEmpty(LIST[i])) break;
                if (LIST[i].StartsWith(" SG_ ") && LIST[i].Split(' ').LastOrDefault().Contains(networkNode.Name))
                {
                    var lineBase = LIST[i].Split(":");
                    if (lineBase.Length != 2) throw new Exception("File is corrupted!");

                    var signalName = lineBase[0].Split(" ")[2];
                    var info = lineBase[1].Split(" ")[1];
                    var info1 = info.Split("|");
                    var signal = new CanSignalBMO();
                    signal.Name = signalName;
                    signal.StartBit = ushort.Parse(info1[0]);
                    signal.Length = ushort.Parse(info1[1].Split("@")[0]);
                    list.Add(signal);
                }
                else if (LIST[i].StartsWith(" SG_ ") && !LIST[i].Split(' ').LastOrDefault().Contains(networkNode.Name)) continue; // these ones should go maybe in a seperate list?
                else
                    throw new Exception("File is corrupted.");
            }
            return list;
        }
    }
}
