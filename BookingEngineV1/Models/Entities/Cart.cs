using BookingEngineV1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tCart")]
    public class Cart
    {
        public long CartID { get; set; }
        public string CompanyID { get; set; }
        public int ChannelID {get;set;}
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfBooking { get; set; }
        public string UserID { get; set; }
        public string Comment { get; set; }
        public List<CartItem> CartItems { get; set; }

        // Unmapped


        [NotMapped]
        public decimal BaseOrDerivedPrice
        {
            get
            {
                return CartItems.Sum(x => x.BaseOrDerivedPrice);
            }
        }

        [NotMapped]
        public decimal TotalPromotion
        {
            get
            {
                return CartItems.Sum(x => x.TotalPromotion);
            }
        }

        [NotMapped]
        public decimal PriceBeforeTax
        {
            get
            {
                return CartItems.Sum(x => x.PriceBeforeTax);
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
        public decimal VAT
        {
            get
            {
                return CartItems.Sum(x => x.VAT);
            }
        }

        [NotMapped]
        public decimal PriceAfterTax
        {
            get
            {
                return CartItems.Sum(x => x.PriceAfterTax);
            }
        }

    }
}
