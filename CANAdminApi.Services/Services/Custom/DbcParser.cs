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

        public DbcParser() 
        { 
            
        }

        public FileBMO LoadFromStream(string fileName, Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            FileBMO obj = new FileBMO();
            obj.FileContent = ReadFully(stream);
            obj.FileName = fileName;
            obj.MimeType = MimeMapping.MimeUtility.GetMimeMapping(fileName);
            obj.Extension = Path.GetExtension(fileName);
            string content;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                obj.NetworkNodes = GetNetworkNodes(reader);
            }

            return obj;
        }

        private byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private IEnumerable<NetworkNodeBMO> GetNetworkNodes(StreamReader content)
        {
            string line;
            IEnumerable<NetworkNodeBMO> list = new HashSet<NetworkNodeBMO>();
            while ((line = content.ReadLine()) != null)
            {
                if(line.StartsWith("BU_: "))
                {
                    var networkNodes = line.Split(' ').ToList(); // start from index >0
                    if (networkNodes.Count > 0) networkNodes.RemoveAt(0); // remove the "BU_:"
                    if (networkNodes.Count == 0) throw new Exception("File does not contain Network Node names.");
                    list = networkNodes.Select(a => new NetworkNodeBMO() { Name = a });
                    foreach(var node in list)
                    {
                        node.CanMessages = GetCanMessages(node, content);
                    }
                }
            }

            return list;
        }

        private IEnumerable<CanMessageBMO> GetCanMessages(NetworkNodeBMO networkNode, StreamReader content)
        {
            string line;
            IEnumerable<CanMessageBMO> list = new HashSet<CanMessageBMO>();
            while ((line = content.ReadLine()) != null)
            {
                if (line.StartsWith("BO_: "))
                {
                    var message = line.Split(' ').ToList(); // start from index >0
                    if (message.Count > 0) message.RemoveAt(0); // remove the "BU_:"
                    if (message.Count == 0) throw new Exception("File does not contain Can Messages.");
                    list = message.Select(a => new CanMessageBMO() { Name = a });
                    foreach (var node in list)
                    {
                        node.CanMessages = GetCanMessages(node, content);
                    }
                }
            }

            return list;
        }

    }
}
