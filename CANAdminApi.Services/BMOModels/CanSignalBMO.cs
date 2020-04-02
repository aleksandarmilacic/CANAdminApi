using System;

namespace CANAdminApi.Services.BMOModels
{
    public class CanSignalBMO : ModelBaseBMO
    {
        public string Name { get; set; }

        public ushort StartBit { get; set; }

        public ushort Length { get; set; }

        public Guid CanMessageId { get; set; }
    }
}