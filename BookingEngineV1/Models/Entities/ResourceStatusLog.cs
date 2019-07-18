using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tResourceStatusLog")]
    public class ResourceStatusLog
    {
        public long ResourceStatusLogId { get; set; }
        public int ResourceID { get; set; }
        public Resource Resource { get; set; }
        public string UserID { get; set; }
        public string Comment { get; set; }
        public int ResourceStatusID { get; set; }
        public DateTime ChangeDate { get; set; }
        public ResourceStatus ResourceStatus { get; set; }
    }
}
