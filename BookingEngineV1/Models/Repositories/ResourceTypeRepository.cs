using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class ResourceTypeRepository : IResourceTypeRepository
    {

        private DataContext context;

        public ResourceTypeRepository(DataContext ctx) => context = ctx;

        public IEnumerable<ResourceType> ResourceTypes => context.ResourceTypes.ToArray();


        // ******************** RESOURCETYPES *******************************

        public void AddResourceType(ResourceType resourceType)
        {
            context.ResourceTypes.Add(resourceType);
            context.SaveChanges();
        }

        public void UpdateResourceType(ResourceType resourceType)
        {
            context.ResourceTypes.Update(resourceType);
            context.SaveChanges();
        }

        public void DeleteResourceType(ResourceType resourceType)
        {
            context.ResourceTypes.Remove(resourceType);
            context.SaveChanges();
        }

        public List<ResourceType> GetAllResourceTypes()
        {
            return context.ResourceTypes.Include(x => x.Resources).ToList();
        }
    }


}

