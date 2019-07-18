using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.DBQueries
{
    public class ResourceTypeUnitsAvailableForSale
    {
        public string CompanyID { get; set; }
        public DateTime DateEffective { get; set; }
        public int ResourceTypeID { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsBlocked { get; set; }
        public int UnitsSold { get; set; }
        public int UnitsAvailableForSale { get; set; }
    }
}
