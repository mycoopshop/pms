using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models.Entities;

namespace BookingEngineV1.Models
{
    public class OfferedResourceType
    {
        [Key]
        public int ResourceTypeID { get; set; }
        public string ResourceTypeName { get; set; }
        public string CompanyID { get; set; }
        public ResourceType ResourceType { get; set; }
        public List<OfferedRateComposition> OfferedRateCompositions { get; set; }
        public int UnitsLeftForSale { get; set; }
    }
}
