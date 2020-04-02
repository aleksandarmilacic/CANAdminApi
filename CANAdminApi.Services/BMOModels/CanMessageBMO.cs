using System;
using System.Collections.Generic;

namespace CANAdminApi.Services.BMOModels
{
    public class CanMessageBMO : ModelBaseBMO
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public Guid NetworkNodeId { get; set; }
         

        public IEnumerable<CanSignalBMO> CanSignals = new HashSet<CanSignalBMO>();
    }
}