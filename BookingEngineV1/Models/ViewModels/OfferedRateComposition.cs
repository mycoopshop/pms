using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models
{
    public class OfferedRateComposition
    {
        [Key]
        public int RateCompositionID { get; set; }
        public string RateCompositionName { get; set; }
        public RateComposition RateComposition { get; set; }
        public string CancellationPolicyName { get; set; }
        public int ResourceTypeID { get; set; }
        public OfferedResourceType OfferedResourceType { get; set; }
        public decimal TotalPriceBeforePromotion { get; set; }
        public decimal TotalEarlyBookingPromotion { get; set; }
        public decimal TotalLastMinutePromotion { get; set; }
        public decimal TotalLongStayPromotion { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AveragePricePerUnit { get; set; }

    }
}
