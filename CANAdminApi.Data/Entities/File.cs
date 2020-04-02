using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Data.Entities
{
    public class File : EntityBase
    {
        public string FileName { get; set; }

        public string MimeType { get; set; }

        public string Extension { get; set; }

        public byte[] FileContent { get; set; }

        public ICollection<NetworkNode> NetworkNodes { get; set; } = new HashSet<NetworkNode>();
    }
}
