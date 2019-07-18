using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingEngineV1.Controllers
{
    public class ResourceTypesController : Controller
    {
        private IResourceTypeRepository repositoryResourceType;

        public ResourceTypesController(IResourceTypeRepository repo) => repositoryResourceType = repo;

        public IActionResult Index(BookingsSelection options)
        {
            return View(repositoryResourceType.GetAllResourceTypes());
        }

        [HttpPost]
        public IActionResult AddResourceType(ResourceType resourceType)
        {
            repositoryResourceType.AddResourceType(resourceType);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult EditResourceType(int resourceTypeID)
        {
            ViewBag.EditId = resourceTypeID;
            return View("Index", repositoryResourceType.ResourceTypes);
        }

        [HttpPost]
        public IActionResult UpdateResourceType(ResourceType resourceType)
        {
            repositoryResourceType.UpdateResourceType(resourceType);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteResourceType(ResourceType resourceType)
        {
            repositoryResourceType.DeleteResourceType(resourceType);
            return RedirectToAction(nameof(Index));
        }
    }
}