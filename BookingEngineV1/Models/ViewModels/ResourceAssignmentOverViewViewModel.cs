using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class ResourceAssignmentOverViewViewModel
    {
        public DateTime DateEffective { get; set; }
        public string CompanyID { get; set; }
        public List<ResourceType> ResourceTypes { get; set; }
        public List<BookingRoomStayResourceAssignment> DateEffectiveBookingRoomStayResourceAssignments { get; set; }
    }
}
