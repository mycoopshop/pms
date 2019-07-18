using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models
{
    [Table("tResource")]
    public class  Resource
    {     
        public int ResourceID { get; set; }
        //public string CompanyID { get; set; }
        public string Name { get; set; }
        public int ResourceTypeID { get; set; }
        public string BedType { get; set; }
        public int? CapacityMax { get; set; }
        public string Description { get; set; }
        public int? Floor { get; set; }
        public int? SizeInM2 { get; set; }
        public ResourceType ResourceType { get; set; }
        //public List<BookingItemResourceAssignment> BookingItemResourceAssignments { get; set; }

    }
}
