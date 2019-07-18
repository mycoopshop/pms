using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tBookingItemDayPromotion")]
    public class BookingItemDayPromotion
    {
        public long BookingItemDayPromotionID { get; set; }
        public long BookingRoomStayDayID { get; set; }
        public string PromotionTypeID { get; set; }
        public decimal Amount { get; set; }
        public BookingRoomStayDay BokingRoomStayDay { get; set; }
    }
}
