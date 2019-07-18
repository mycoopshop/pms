using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    [Table("tInquiry")]
    public class Inquiry
    {
        public long InquiryID { get; set; }
        public string CompanyID { get; set; }
        public int ChannelID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTime DateOfInquiry { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfUnits { get; set; }
    }
}
