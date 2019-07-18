using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tCartItemDay")]
    public class CartItemDay
    {
        public long CartItemDayID { get; set; }
        public long CartItemID { get; set; }
        public CartItem CartItem { get; set; }
        public DateTime DateEffective { get; set; }
        public decimal BaseOrDerivedPricePerUnit { get; set; }
        public decimal TotalPromotionPerUnit { get; set; }

        public decimal VATPercentage {
             get {
                //private DataContext context;
                return 0.037M;
            }
        }
        public decimal PriceBeforeTaxPerUnit
        {
            get
            {
                return PriceAfterTaxPerUnit / (1 + VATPercentage);
            }
        }

        public decimal PriceAfterTaxPerUnit
        {
            get
            {
                return BaseOrDerivedPricePerUnit + TotalPromotionPerUnit;
            }
        }
        public decimal VATPerUnit
        {
            get
            {
                return VATPercentage * PriceAfterTaxPerUnit;
            }
        }

        [NotMapped]
        public decimal LastMinutePromotionPerUnit {
            get{
                if (CartItemDayPromotions == null)
                {
                    return 0;
                }

                CartItemDayPromotion ciDP = new CartItemDayPromotion();
                ciDP = CartItemDayPromotions.Where(x => x.PromotionTypeID == "PT02").SingleOrDefault();
                if (ciDP != null)
                {
                    return ciDP.Amount;
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public decimal EarlyBookingPromotionPerUnit
        {
            get
            {
                if (CartItemDayPromotions == null)
                {
                    return 0;
                }

                var prom = CartItemDayPromotions.SingleOrDefault(x => x.PromotionTypeID == "PT01");
                if (prom == null)
                {
                    return 0;
                }
                else
                {
                    return CartItemDayPromotions.Where(x => x.PromotionTypeID == "PT01").SingleOrDefault().Amount;
                }                
            }
        }

        [NotMapped]
        public decimal LongStayPromotionPerUnit
        {
            get
            {
                if (CartItemDayPromotions == null)
                {
                    return 0;
                }

                CartItemDayPromotion ciDP = new CartItemDayPromotion();
                ciDP = CartItemDayPromotions.Where(x => x.PromotionTypeID == "PT03").SingleOrDefault();
                if (ciDP != null)
                {
                    return ciDP.Amount;
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public decimal TotalPromotion
        {
            get
            {
                return TotalPromotionPerUnit * this.CartItem.NumberOfUnits;
            }
        }

        [NotMapped]
        public decimal PriceBeforeTax
        {
            get
            {
                return PriceBeforeTaxPerUnit * this.CartItem.NumberOfUnits;
            }
        }

        [NotMapped]
        public decimal PriceAfterTax
        {
            get
            {
                return PriceAfterTaxPerUnit * this.CartItem.NumberOfUnits;
            }
        }


        [NotMapped]
        public List<CartItemDayPromotion> CartItemDayPromotions { get; set; }

    }
}
