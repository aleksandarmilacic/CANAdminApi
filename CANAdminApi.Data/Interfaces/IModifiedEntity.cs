
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CANAdminApi.Data.Interfaces
{
    public interface IModifiedEntity : ICreatedEntity
    {
        DateTime Modified { get; set; }
        
    }
}
