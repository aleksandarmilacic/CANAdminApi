using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Data.Interfaces
{
    public interface IDeletableEntity
    {
        DateTime? HasBeenDeleted { get; set; }
    }
}
