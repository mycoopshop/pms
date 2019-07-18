using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models
{
    public class BookingsSelection
    {
        public DateTime DateOfBooking { get; set; }
        public string DateOfBookingOperator { get; set; }
        public string DateOfDepartureOperator { get; set; }
        public string DateOfArrivalOperator { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public long? BookingID { get; set; }
        public string CompanyID { get; set; }
        public int ChannelID { get; set; }
        public int ResourceTypeID { get; set; }
        public int RateCompositionID { get; set; }
        public string GuestNames { get; set; }


    }
}
