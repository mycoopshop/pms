using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class EditBookingViewModel
    {
        public Cart Cart { get; set; }
        public List<ResourceType> ResourceTypes { get; set; }
        public List<RateComposition> RateCompositions { get; set; }
    }
}
