using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IInventoryRepository
    {
        List<ResourceStock> GetResourceStocks();
        ResourceStock GetResourceStock(int resourceStockID);
        ResourceBlock GetResourceBlock(int resourceBlockID);
        List<ResourceBlock> GetResourceBlocks();
        List<ResourceType> GetResourceTypes();
        void UpdateResourceStock(ResourceStock resourceStock);
        void AddResourceStock(ResourceStock resourceStock);
        void AddResourceBlock(ResourceBlock resourceBlock);
        List<Resource> GetResources();
        void DeleteResourceStock(ResourceStock resourceStock);
        void DeleteResourceBlock(ResourceBlock resourceBlock);
    }
}
