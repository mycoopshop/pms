using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class CartItemDayRateDetails
    {
        public int ResourceRate_SK { get; set; }
        public string CompanyID { get; set; }
        public int ResourceTypeID { get; set; }
        public int RateCompositionID { get; set; }
        public int UnitsAvailableForSale { get; set; }
        public int UnitsSold { get; set; }
        public int UnitsLeftForSale { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime DateEffective { get; set; }
        public decimal ParentPrice { get; set; }
        public decimal ChangeAmount { get; set; }
        public decimal ChangePercentage { get; set; }
        public decimal DerivedPrice { get; set; }
        public decimal LastMinutePromotion { get; set; }
        public decimal EarlyBookingPromotion { get; set; }
        public decimal LongStayPromotion { get; set; }

    }
}
