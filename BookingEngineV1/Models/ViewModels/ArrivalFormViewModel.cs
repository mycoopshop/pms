using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class ArrivalFormViewModel
    {
        public BookingRoomStay BookingRoomStay { get; set; }
        public Booking Booking { get; set; }
        public String Signature { get; set; }
        public String Title { get; set; }
    }
}
