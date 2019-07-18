using BookingEngineV1.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private DataContext context;

        public ResourceRepository(DataContext ctx) => context = ctx;

        public IEnumerable<Resource> Resources => context.Resources.Include(r => r.ResourceType).ToArray();
        //public IEnumerable<ResourceType> ResourceTypes => context.ResourceTypes.ToArray();
        //public IEnumerable<BookingTemp>BookingTemps => context.BookingTemps.Include(bt => bt.BookingTempItems).ThenInclude(bi=>bi.ResourceType);
        //public IEnumerable<BookingTempItem> BookingTempItems => context.BookingTempItems.ToArray();

        public Resource GetResource(long ResourceID)
            => context.Resources.Include(rt => rt.ResourceType).First(r => r.ResourceID == ResourceID);


        public void AddResource(Resource resource)
        {
            this.context.Resources.Add(resource);
            this.context.SaveChanges();
        }

        public void UpdateResource(Resource resource)
        {
            Resource r = context.Resources.Find(resource.ResourceID);
            r.Name = resource.Name;
            r.ResourceTypeID = resource.ResourceTypeID;
            r.Floor = resource.Floor;
            r.SizeInM2 = resource.SizeInM2;
            r.BedType = resource.BedType;
            r.CapacityMax = resource.CapacityMax;
            r.Description = resource.Description;
            //context.Resources.Update(resource);
            context.SaveChanges();
        }

        public void UpdateAll(Resource[] resources)
        {
            //context.Resources.UpdateRange(resources);

            Dictionary<int, Resource> data = resources.ToDictionary(r => r.ResourceID);
            IEnumerable<Resource> baseline = context.Resources.Where(r => data.Keys.Contains(r.ResourceID));

            foreach (Resource databaseResource in baseline)
            {
                Resource requestResource = data[databaseResource.ResourceID];
                databaseResource.Name = requestResource.Name;
                databaseResource.ResourceTypeID = requestResource.ResourceTypeID;
                databaseResource.Description = requestResource.Description;
            }
            context.SaveChanges();
        }

        public void Delete(Resource resource)
        {
            context.Resources.Remove(resource);
            context.SaveChanges();
        }
    }
}
