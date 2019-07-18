using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class CurrentResourceStatusViewModel
    {
        [Key]
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        //public Resource Resource { get; set; }
        public int ResourceTypeID { get; set; }
        public string ResourceTypeName { get; set; }
        public string UserID { get; set; }
        public string Comment { get; set; }
        public int? ResourceStatusID { get; set; }
        public string ResourceStatusName { get; set; }
        public DateTime? ChangeDate { get; set; }
        //public ResourceStatus ResourceStatus { get; set; }
    }
}
