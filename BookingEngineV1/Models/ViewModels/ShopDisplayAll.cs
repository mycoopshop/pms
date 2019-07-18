using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.DBViews
{
    public class ShopDisplayAll
    {
        public int ResourceRate_SK { get; set; }
        public string CompanyID { get; set; }
        public int ResourceTypeID { get; set; }
        public string ResourceTypeName { get; set; }
        public int RateCompositionID { get; set; }
        public string RateCompositionName { get; set; }
        public string CancellationPolicyName { get; set; }
        public int UnitsLeftForSale { get; set; }
        public decimal TotalPriceBeforePromotion { get; set; }
        public decimal TotalEarlyBookingPromotion { get; set; }
        public decimal TotalLastMinutePromotion { get; set; }
        public decimal TotalLongStayPromotion { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AveragePricePerUnit { get; set; }

    }
}
