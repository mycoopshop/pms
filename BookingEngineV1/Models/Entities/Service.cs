using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tService")]
    public class Service
    {
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

    }
}
