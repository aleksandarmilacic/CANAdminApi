using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CANAdminApi.Data.Entities
{
    public class CanSignal : EntityBase
    {
        public string Name { get; set; }

        public ushort StartBit { get; set; }

        public ushort Length { get; set; }

        public Guid CanMessageId { get; set; }

        [ForeignKey(nameof(CanMessageId))]
        public virtual CanMessage CanMessage { get; set; }
    }
}
