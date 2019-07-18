using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class ShopDisplay
    {
        public Inquiry Inquiry { get; set; }
        public List<OfferedResourceType> OfferedResourceTypes { get; set; }
        public Cart Cart { get; set; }

    }
}
