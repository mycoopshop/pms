using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tChannel")]
    public class Channel
    {
        [Key]
        public int ChannelID { get; set; }
        public string Name { get; set; }
    }
}
