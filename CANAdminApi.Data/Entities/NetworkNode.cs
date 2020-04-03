using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CANAdminApi.Data.Entities
{
    public class NetworkNode : EntityBase
    {
        public string Name { get; set; }

        public Guid FileId { get; set; }

        [ForeignKey(nameof(FileId))]
        public virtual File File { get; set; }

        public virtual ICollection<CanMessage> CanMessages { get; set; } = new HashSet<CanMessage>();
    }
}
