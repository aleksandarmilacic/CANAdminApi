using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.DTOModels
{
    public abstract class TimestampBaseDTO
    {
        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}
