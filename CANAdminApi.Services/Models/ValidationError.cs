using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.Models
{
    public class ValidationError
    {
        public string ErrorMessage { get; set; }

        public List<string> PropertyNames { get; }
    }
}
