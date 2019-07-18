using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class ResourceStatusChangeViewModel
    {
        public List<CurrentResourceStatusViewModel> CurrentResourceStatuses { get; set; }
        public List<ResourceStatus> ResourceStatusesAvailable { get; set; }
    }
}
