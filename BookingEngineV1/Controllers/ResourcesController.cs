using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookingEngineV1.Models;
using BookingEngineV1.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BookingEngineV1.Controllers
{
    [Authorize]
    public class ResourcesController : Controller
    {
        private IResourceRepository resourceRepository;
        private IResourceTypeRepository resourceTypeRepository;

        public ResourcesController(IResourceRepository repoR, IResourceTypeRepository repoRT)
        {
            resourceRepository = repoR;
            resourceTypeRepository = repoRT;
        }

        public IActionResult Index() => View(resourceRepository.Resources);

     
        public IActionResult UpdateResource(long ResourceID)
        {
            ViewBag.ResourceTypes = resourceTypeRepository.ResourceTypes;
            return View(ResourceID ==0? new Resource(): resourceRepository.GetResource(ResourceID));
        }

        [HttpPost]
        public IActionResult UpdateResource(Resource resource)
        {
            if(resource.ResourceID == 0)
            {
                resourceRepository.AddResource(resource);
            }
            else
            {
                resourceRepository.UpdateResource(resource);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAll()
        {
            ViewBag.UpdateAll = true;
            return View(nameof(Index), resourceRepository.Resources);
        }

        [HttpPost]
        public IActionResult UpdateAll(Resource[] resources)
        {
            resourceRepository.UpdateAll(resources);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Resource resource)
        {
            resourceRepository.Delete(resource);
            return RedirectToAction(nameof(Index));
        }

    }
}