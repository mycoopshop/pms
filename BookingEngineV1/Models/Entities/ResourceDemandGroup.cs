using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tResourceDemandGroup")]
    public class ResourceDemandGroup
    {
        public int ResourceDemandGroupID { get; set; }
        public string Name { get; set; }
        public string CompanyId { get; set; }
        public IEnumerable<ResourceType> ResourceTypes { get; set; }

    }
}
