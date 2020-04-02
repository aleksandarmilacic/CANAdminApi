using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.DTOModels
{
    public class ModelBaseDTO : TimestampBaseDTO
    {
        public Guid Id { get; set; }
    }
}
