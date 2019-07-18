using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tRateCompositionItem")]
    public class RateCompositionItem
    {
        public int RateCompositionItemID { get; set; }
        public int ServiceID { get; set; }
        public int RateCompositionID { get; set; }
        public Service Service { get; set; }
   
    }
}
