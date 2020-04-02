
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CANAdminApi.Data.Interfaces
{
    public interface ICreatedEntity
    {
        DateTime Created { get; set; }
      
    }
}
