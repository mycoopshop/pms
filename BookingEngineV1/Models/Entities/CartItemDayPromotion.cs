using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tCartItemDayPromotion")]
    public class CartItemDayPromotion
    {
        public long CartItemDayPromotionID { get; set; }
        public DateTime DateEffective { get; set; }
        public string PromotionTypeID { get; set; }
        public decimal Amount { get; set; }
        public long CartItemDayID { get; set; }
        public CartItemDay CartItemDay { get; set; }

    }
}
