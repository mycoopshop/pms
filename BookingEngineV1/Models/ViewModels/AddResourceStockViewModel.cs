using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class AddResourceStockViewModel
    {
        public List<Resource> Resources { get; set; }
        public int? ResourceID { get; set; }
        public ResourceStock ResourceStock { get; set; }
    }
}
