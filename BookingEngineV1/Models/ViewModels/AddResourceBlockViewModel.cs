using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class AddResourceBlockViewModel
    {
        public List<Resource> Resources { get; set; }
        public int? ResourceID { get; set; }
        public ResourceBlock ResourceBlock { get; set; }
    }
}
