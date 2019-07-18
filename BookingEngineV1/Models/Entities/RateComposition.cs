using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tRateComposition")]
    public class RateComposition
    {
        public int RateCompositionID { get; set; }
        public string Name { get; set; }
        public string CompanyID { get; set; }
        public int CancellationPolicyID { get; set; }
        //public List<RateCompositionItem> RateCompositionItems { get; set; }
    }
}
