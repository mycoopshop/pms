using BookingEngineV1.Models.DBQueries;
using BookingEngineV1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IAvailableResourceTypesRepository
    {
        List<OfferedResourceType> GetOfferedResourceTypes(Inquiry inquiry);
        List<ResourceTypeUnitsAvailableForSale> GetResourceTypesNumberOfUnitsAvailable(Inquiry inquiry);
    }
}
