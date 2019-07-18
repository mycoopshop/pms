using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Entities
{
    [Table("tBookingRoomStayResourceAssignment")]
    public class BookingRoomStayResourceAssignment
    {
        public long BookingRoomStayResourceAssignmentID { get; set; }
        public long BookingRoomStayID { get; set; }
        public BookingRoomStay BookingRoomStay { get; set; }
        public int ResourceID { get; set; }
        public Resource Resource { get; set; }
        public string Comment { get; set; }
        
    }
}
