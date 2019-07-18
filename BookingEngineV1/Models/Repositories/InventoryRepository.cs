using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DataContext context;

        public InventoryRepository(DataContext ctx) => context = ctx;

        public List<ResourceStock> GetResourceStocks()
        {
            return context.ResourceStocks
                .Include(x=>x.Resource)
                .ThenInclude(x=>x.ResourceType)
                .ToList();
        }

        public List<ResourceBlock> GetResourceBlocks()
        {
            return context.ResourceBlocks
                .Include(x=>x.Resource)
                .ThenInclude(x=>x.ResourceType)
                .ToList();
        }

        public List<ResourceType> GetResourceTypes()
        {
            return context.ResourceTypes.ToList();
        }

        public void UpdateResourceStock(ResourceStock resourceStock)
        {
            ResourceStock rs = new ResourceStock();
            rs = context.ResourceStocks.Where(x => x.ResourceStockID == resourceStock.ResourceStockID).SingleOrDefault();
            rs.DateFrom =resourceStock.DateFrom;
            rs.DateTo = resourceStock.DateTo;

            context.SaveChanges();
         }

        public void AddResourceStock(ResourceStock resourceStock)
        {
            this.context.ResourceStocks.Add(resourceStock);
            this.context.SaveChanges();
        }

        public void AddResourceBlock(ResourceBlock resourceBlock)
        {
            context.ResourceBlocks.Add(resourceBlock);
            context.SaveChanges();
        }

        public ResourceStock GetResourceStock(int resourceStockID)
        {
            return context.ResourceStocks.Where(x => x.ResourceStockID == resourceStockID)
                .Include(x=>x.Resource)
                .SingleOrDefault();
        }

        public ResourceBlock GetResourceBlock(int resourceBlockID)
        {
            return context.ResourceBlocks.Where(x => x.ResourceBlockID == resourceBlockID)
                .Include(x => x.Resource)
                .SingleOrDefault();
        }

        public List<Resource> GetResources()
        {
            return context.Resources.ToList();
        }

        public void DeleteResourceStock(ResourceStock resourceStock)
        {
            context.ResourceStocks.Remove(resourceStock);
            context.SaveChanges();
        }

        public void DeleteResourceBlock(ResourceBlock resourceBlock)
        {
            context.ResourceBlocks.Remove(resourceBlock);
            context.SaveChanges();
        }
    }
}
