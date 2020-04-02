using CANAdminApi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CANAdminApi.Data.Entities
{
    public abstract class EntityBase : IIdEntity, IDeletableEntity, IModifiedEntity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        public DateTime? HasBeenDeleted { get; set; }
        public DateTime Modified { get; set; } = DateTime.UtcNow;

   

        public DateTime Created { get; set; }

    
    }
}
