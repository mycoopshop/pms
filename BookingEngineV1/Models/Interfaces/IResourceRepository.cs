using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IResourceRepository
    {
        IEnumerable<Resource> Resources { get; }

        void AddResource(Resource resource);
        void UpdateResource(Resource resource);
        Resource GetResource(long ResourceID);
        void UpdateAll(Resource[] resources);
        void Delete(Resource resource);
    }
}
