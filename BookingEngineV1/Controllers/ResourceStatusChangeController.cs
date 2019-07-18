using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingEngineV1.Controllers
{
    public class ResourceStatusChangeController : Controller
    {
        private readonly IResourceStatusRepository resourceStatusRepository;

        public ResourceStatusChangeController(IResourceStatusRepository statusRepo)
        {
            resourceStatusRepository = statusRepo;
        }


        public IActionResult Index()
        {

            ResourceStatusChangeViewModel vm = new ResourceStatusChangeViewModel()
            {
                CurrentResourceStatuses = resourceStatusRepository.GetAllResourceStatuses(),
                ResourceStatusesAvailable = resourceStatusRepository.GetResourceStatuses()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddResourceStatusChange(int resourceID, int resourceStatusID)
        {
            resourceStatusRepository.AddResourceStatusChange(resourceID, resourceStatusID, "test", "no comment");

            ResourceStatusChangeViewModel vm = new ResourceStatusChangeViewModel()
            {
                CurrentResourceStatuses = resourceStatusRepository.GetAllResourceStatuses(),
                ResourceStatusesAvailable = resourceStatusRepository.GetResourceStatuses()
            };

            return View(nameof(Index),vm);
        }
    }
}