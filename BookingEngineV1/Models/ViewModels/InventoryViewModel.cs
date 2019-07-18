using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.ViewModels
{
    public class InventoryViewModel
    {
        public List<ResourceStock> ResourceStocks { get; set; }
        public List<ResourceBlock> ResourceBlocks { get; set; }
        public List<ResourceType> ResourceTypes { get; set; }
    }
}
