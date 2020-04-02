using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.DTOModels
{
    public class FileDTO : ModelBaseDTO
    {
        public string FileName { get; set; }

        public string MimeType { get; set; }

        public string Extension { get; set; }
        

        public ICollection<NetworkNodeDTO> NetworkNodes { get; set; } = new HashSet<NetworkNodeDTO>();
    }
}
