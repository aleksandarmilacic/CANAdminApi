namespace CANAdminApi.Services.DTOModels
{
    public class CanSignalDTO : ModelBaseDTO
    {
        public string Name { get; set; }

        public ushort StartBit { get; set; }

        public ushort Length { get; set; }
    }
}