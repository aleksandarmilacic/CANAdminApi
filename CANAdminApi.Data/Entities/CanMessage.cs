using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CANAdminApi.Data.Entities
{
    public class CanMessage : EntityBase
    {

        [Column("Identity")]
        public long ID { get; set; }

        public string Name { get; set; }

        public Guid NetworkNodeId { get; set; }

        [ForeignKey(nameof(NetworkNodeId))]
        public virtual NetworkNode NetworkNode { get; set; }

        public virtual ICollection<CanSignal> CanSignals { get; set; } = new HashSet<CanSignal>();
    }
}
