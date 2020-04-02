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
        public NetworkNode NetworkNode { get; set; }

        public ICollection<CanSignal> CanSignals = new HashSet<CanSignal>();
    }
}
