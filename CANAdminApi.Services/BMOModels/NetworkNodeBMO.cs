using System;
using System.Collections.Generic;

namespace CANAdminApi.Services.BMOModels
{
    public class NetworkNodeBMO : ModelBaseBMO
    {
        public string Name { get; set; }

        public Guid FileId { get; set; } 

        public IEnumerable<CanMessageBMO> CanMessages = new HashSet<CanMessageBMO>();
    }
}