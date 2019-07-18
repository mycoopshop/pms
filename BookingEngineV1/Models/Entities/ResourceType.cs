using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace BookingEngineV1.Models.Entities
{
    [Table("tResourceType")]
    public class ResourceType
    {
        public int ResourceTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Resource> Resources { get; set; }
        public int ResourceDemandGroupId { get; set; }
        public ResourceDemandGroup ResourceDemandGroup { get; set; }
        //public Guid ResourceTypeID_SK { get; set; }
    }
}
