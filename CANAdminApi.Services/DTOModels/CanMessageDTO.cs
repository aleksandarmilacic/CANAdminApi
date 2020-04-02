using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.DTOModels
{
    public class CanMessageDTO : ModelBaseDTO
    {
        public long ID { get; set; }

        public string Name { get; set; }
 

        public IEnumerable<CanSignalDTO> CanSignals = new HashSet<CanSignalDTO>();
    }
}
