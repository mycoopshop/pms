using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class BookingViewModel
    {
        public List<Booking> Bookings { get; set; }
        public BookingsSelection BookingSelection { get; set; }
        public List<string> Operators { get
            {
                return new List<string>(new String[] {"", "=", ">=", "<=" });
            }
        }
        public List<Channel> Channels { get; set; }
        public List<ResourceType> ResourceTypes { get; set; }
        public List<RateComposition> RateCompositions { get; set; }
   
        
    }
}
