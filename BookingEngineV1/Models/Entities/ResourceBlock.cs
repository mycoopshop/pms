using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tResourceBlock")]
    public class ResourceBlock
    {
        public int ResourceBlockID { get; set; }
        public int ResourceID { get; set; }
        public virtual Resource Resource { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }
}
