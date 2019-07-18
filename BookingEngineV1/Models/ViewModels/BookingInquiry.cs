using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class BookingInquiry
    {
        public string CompanyID { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfInquiry { get; set; }
    }
}
