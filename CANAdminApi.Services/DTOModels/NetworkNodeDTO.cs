using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.DTOModels
{
    public class NetworkNodeDTO : ModelBaseDTO
    {
        public string Name { get; set; }

        public IEnumerable<CanMessageDTO> CanMessages = new HashSet<CanMessageDTO>();
    }
}
