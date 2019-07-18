using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingEngineV1.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryRepository inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepo)
        {
            inventoryRepository = inventoryRepo;

        }


        public IActionResult Index()
        {
            InventoryViewModel vm = new InventoryViewModel()
            {
                ResourceStocks = inventoryRepository.GetResourceStocks(),
                ResourceBlocks = inventoryRepository.GetResourceBlocks(),
                ResourceTypes = inventoryRepository.GetResourceTypes()
            };

            List<ResourceStock> rs = inventoryRepository.GetResourceStocks();

            return View(rs);
        }

        public IActionResult Blocks()
        {
            List<ResourceBlock> rb = inventoryRepository.GetResourceBlocks();

            return View(rb);
        }

        public IActionResult UpdateResourceStock(int resourceStockID)
        {

            return View(resourceStockID == 0 ? new ResourceStock() : inventoryRepository.GetResourceStock(resourceStockID));
        }

        [HttpPost]
        public IActionResult UpdateResourceStock(ResourceStock resourceStock)
        {
            if (resourceStock.ResourceStockID == 0)
            {
                inventoryRepository.AddResourceStock(resourceStock);
            }
            else
            {
                inventoryRepository.UpdateResourceStock(resourceStock);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddResourceStock(ResourceStock resourceStock)
        {
            inventoryRepository.AddResourceStock(resourceStock);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddResourceStock()
        {
            AddResourceStockViewModel vm = new AddResourceStockViewModel();
            vm.Resources = inventoryRepository.GetResources();
            vm.ResourceStock = new ResourceStock();
            vm.ResourceStock.ResourceID = 0;
            return View(vm);
        }

        [HttpGet]
        public IActionResult AddResourceBlock()
        {
            AddResourceBlockViewModel vm = new AddResourceBlockViewModel();
            vm.Resources = inventoryRepository.GetResources();
            vm.ResourceBlock = new ResourceBlock();
            vm.ResourceBlock.ResourceID = 0;
            return View(vm);
        }


        [HttpPost]
        public IActionResult AddResourceBlock(ResourceBlock resourceBlock)
        {
            inventoryRepository.AddResourceBlock(resourceBlock);
            return RedirectToAction(nameof(Blocks));
        }


        public IActionResult DeleteResourceStock(int resourceStockID)
        {
            ResourceStock rs = inventoryRepository.GetResourceStock(resourceStockID);
            inventoryRepository.DeleteResourceStock(rs);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteResourceBlock(int resourceBlockID)
        {
            ResourceBlock rb = inventoryRepository.GetResourceBlock(resourceBlockID);
            inventoryRepository.DeleteResourceBlock(rb);

            return RedirectToAction(nameof(Blocks));
        }
    }
}