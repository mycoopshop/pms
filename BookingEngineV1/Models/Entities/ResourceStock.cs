using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tResourceStock")]
    public class ResourceStock
    {
        public int ResourceStockID { get; set; }
        public int ResourceID { get; set; }
        public Resource Resource { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    }
}
