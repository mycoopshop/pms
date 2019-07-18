using BookingEngineV1.Models.DBQueries;
using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class CreateBookingViewModel
    {
        public Inquiry Inquiry { get; set; }
        public Cart Cart { get; set; }
        public string CompanyID { get; set; }
        public int ChannelID { get; set; }
        public int ResourceTypeID { get; set; }
        public int RateCompositionID { get; set; }
        public int NumberOfUnits { get; set; }
        public string GuestNames { get; set; }
        public string UserID { get; set; }
        public List<Channel> Channels { get; set; }
        public List<ResourceType> ResourceTypes { get; set; }
        public List<RateComposition> RateCompositions { get; set; }
        public List<ResourceTypeUnitsAvailableForSale> ResourceTypeUnitsAvailableForSale { get; set; }
        public List<OfferedResourceType> OfferedResourceTypes { get; set; }
    }
}
