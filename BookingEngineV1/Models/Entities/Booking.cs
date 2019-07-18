using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models
{
    [Table("tBooking")]
    public class Booking
    {
        [Key]
        public long BookingIDPMS { get; set; }
        public string CompanyID { get; set; }
        public int ChannelID { get; set; }
        public DateTime DateOfBooking { get; set; }
        //public DateTime DateOfArrival { get; set; }
        //public DateTime DateOfDeparture { get; set; }
        public string BookingIdChannelManager { get; set; }
        public decimal? Commission { get; set; }
        public string GuestNames { get; set; }
        public string CountryID { get; set; }
        public string UserID { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public IEnumerable<BookingRoomStay> BookingRoomStays { get; set; }

        public Channel Channel { get; set; }
        //public IEnumerable<Channel> Channels { get; set; }

        [NotMapped]
        public decimal BaseOrDerivedPrice
        {
            get
            {
                return BookingRoomStays.Sum(x => x.BaseOrDerivedPrice);
            }
        }

        [NotMapped]
        public decimal TotalPromotion
        {
            get
            {
                return BookingRoomStays.Sum(x => x.TotalPromotion);
            }
        }

        [NotMapped]
        public decimal PriceBeforeTax
        {
            get
            {
                return BookingRoomStays.Sum(x => x.PriceBeforeTax);
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
                return BookingRoomStays.Sum(x => x.VAT);
            }
        }

        [NotMapped]
        public decimal PriceAfterTax
        {
            get
            {
                return BookingRoomStays.Sum(x => x.PriceAfterTax);
            }
        }

    }
}
