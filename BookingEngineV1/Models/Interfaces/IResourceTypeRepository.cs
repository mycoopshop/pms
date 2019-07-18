using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IResourceTypeRepository
    {

        IEnumerable<ResourceType> ResourceTypes { get; }

        List<ResourceType> GetAllResourceTypes();
        void AddResourceType(ResourceType resourceType);
        void UpdateResourceType(ResourceType resourceType);
        void DeleteResourceType(ResourceType resourceType);
    }
}
