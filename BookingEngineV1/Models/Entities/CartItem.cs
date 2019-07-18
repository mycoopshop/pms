using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tCartItem")]
    public class CartItem
    {
        public long CartItemID { get; set; }
        public long CartID { get; set; }
        public Cart Cart { get; set; }
        public int ResourceTypeID { get; set; }
        public ResourceType ResourceType { get; set; }
        public int RateCompositionID { get; set; }
        public RateComposition RateComposition { get; set; }
        [NotMapped]
        public List<RateCompositionItem> RateCompositionItems { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public int NumberOfUnits { get; set; }
        public int NumberOfGuests { get; set; }
        [NotMapped]
        public int NumberOfNights
        {
            get
            {
                return (int)(DateOfDeparture - DateOfArrival).TotalDays;
            }
        }
        public List<CartItemDay> CartItemDays { get; set; }
        //unmapped

        [NotMapped]
        public decimal BaseOrDerivedPricePerUnit
        {
            get
            {
                return CartItemDays.Sum(x => x.BaseOrDerivedPricePerUnit);
            }
        }

        [NotMapped]
        public decimal BaseOrDerivedPrice
        {
            get
            {
                return NumberOfUnits * BaseOrDerivedPricePerUnit;
            }
        }

        [NotMapped]
        public decimal LastMinutePromotionPerUnit
        {
            get
            {
                return CartItemDays.Sum(x => x.LastMinutePromotionPerUnit);
            }
        }

        [NotMapped]
        public decimal EarlyBookingPromotionPerUnit
        {
            get
            {
                return CartItemDays.Sum(x => x.EarlyBookingPromotionPerUnit);
            }
        }

        [NotMapped]
        public decimal LongStayPromotionPerUnit
        {
            get
            {
                return CartItemDays.Sum(x => x.LongStayPromotionPerUnit);
            }
        }

        [NotMapped]
        public decimal TotalPromotionPerUnit
        {
            get
            {
                return CartItemDays.Sum(x => x.TotalPromotionPerUnit);
            }
        }

        [NotMapped]
        public decimal TotalPromotion
        {
            get
            {
                return NumberOfUnits * TotalPromotionPerUnit;
            }
        }

        [NotMapped]
        public decimal PriceBeforeTaxPerUnit
        {
            get
            {
                return PriceAfterTaxPerUnit - VATPerUnit;
            }
        }

        [NotMapped]
        public decimal PriceBeforeTax
        {
            get
            {
                return NumberOfUnits * PriceBeforeTaxPerUnit;
            }
        }

        [NotMapped]
        public decimal VATPercentage
        {
            get
            {
                return 0.037M;
            }
        }

        [NotMapped]
        public decimal VATPerUnit
        {
            get
            {
                return CartItemDays.Sum(x => x.VATPerUnit);
            }
        }

        [NotMapped]
        public decimal VAT
        {
            get
            {
                return NumberOfUnits * VATPerUnit;
            }
        }

        [NotMapped]
        public decimal PriceAfterTaxPerUnit
        {
            get
            {
                return BaseOrDerivedPricePerUnit + TotalPromotionPerUnit;
            }
        }

        [NotMapped]
        public decimal PriceAfterTax
        {
            get
            {
                return NumberOfUnits * PriceAfterTaxPerUnit;
            }
        }

    }
}
