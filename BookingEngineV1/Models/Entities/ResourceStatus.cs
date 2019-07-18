using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tResourceStatus")]
    public class ResourceStatus
    {
        public int ResourceStatusID { get; set; }
        public string Name { get; set; }
    }
}
