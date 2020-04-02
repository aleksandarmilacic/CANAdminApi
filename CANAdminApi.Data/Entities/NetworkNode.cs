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
        public File File { get; set; }

        public ICollection<CanMessage> CanMessages = new HashSet<CanMessage>();
    }
}
