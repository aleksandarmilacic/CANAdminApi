using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.BMOModels
{
    public class FileBMO : ModelBaseBMO
    {
        public string FileName { get; set; }

        public string MimeType { get; set; }

        public string Extension { get; set; }

        public byte[] FileContent { get; set; }

        public IEnumerable<NetworkNodeBMO> NetworkNodes { get; set; } = new HashSet<NetworkNodeBMO>();
    }
}
