using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.Repositories;
using BookingEngineV1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingEngineV1.Controllers
{
    public class ResourceAssignmentController : Controller
    {
        private readonly IBookingItemResourceAssignmentRepository resourceAssignmentRepository;
        private readonly IResourceTypeRepository resourceTypeRepository;

        public ResourceAssignmentController(IBookingItemResourceAssignmentRepository resourceAssignmentRepo, IResourceTypeRepository resourceTypeRepo)
        {
            resourceAssignmentRepository = resourceAssignmentRepo;
            resourceTypeRepository = resourceTypeRepo;
        }

        public IActionResult Index(string companyID, DateTime? dateOfArrival = null)
        {
            string pCompanyID = (companyID  ?? "B68162");
            DateTime pDateOfArrival = dateOfArrival  ?? DateTime.Now;
            //DateTime dateOfArrival = DateTime.Parse("2018-05-03");
            ResourceAssignmentViewModel vm = new ResourceAssignmentViewModel()
            {
                DateOfArrival = pDateOfArrival,
                CompanyID = pCompanyID,
                BookingItemsByArrivalDate = resourceAssignmentRepository.GetBookingItemsByArrivalDate(pCompanyID, pDateOfArrival),
                BookingItemsResourceAssignments = resourceAssignmentRepository.GetBookingItemResourceAssignments(companyID, pDateOfArrival),
                //ResourcesAvailableForAssignment = resourceAssignmentRepository.GetResourcesAvailableForAssignment(pCompanyID, pDateOfArrival)
            };
            return View(vm);
        }

        public IActionResult AddResourceAssignment(string companyID, DateTime dateOfArrival, long bookingRoomStayID, int resourceID)
        {

            string comment = "I was testing";
            ResourceAssignmentViewModel vm = new ResourceAssignmentViewModel()
            {
               BookingItemsResourceAssignments = resourceAssignmentRepository.AssignResourceToBookingItem(resourceID, bookingRoomStayID, comment),
               DateOfArrival = dateOfArrival,
               CompanyID = companyID,
               BookingItemsByArrivalDate = resourceAssignmentRepository.GetBookingItemsByArrivalDate(companyID, dateOfArrival),
                //ResourcesAvailableForAssignment = resourceAssignmentRepository.GetResourcesAvailableForAssignment(companyID, dateOfArrival)
            };

            return View(nameof(Index), vm);
        }

        [HttpPost]
        public IActionResult RemoveResourceAssignment(BookingRoomStayResourceAssignment biRa, DateTime dateOfArrival, string companyID)
        {
            resourceAssignmentRepository.RemoveBookignItemResourceAssignement(biRa);

            //string comment = "I was testing";
            ResourceAssignmentViewModel vm = new ResourceAssignmentViewModel()
            {
                DateOfArrival = dateOfArrival,
                CompanyID = companyID,
                BookingItemsByArrivalDate = resourceAssignmentRepository.GetBookingItemsByArrivalDate(companyID, dateOfArrival),
                //ResourcesAvailableForAssignment = resourceAssignmentRepository.GetResourcesAvailableForAssignment(companyID, dateOfArrival),
                BookingItemsResourceAssignments = resourceAssignmentRepository.GetBookingItemResourceAssignments(companyID, dateOfArrival)
            };

            return View(nameof(Index), vm);
        }

        public IActionResult ResourceAssignmentOverView(string companyID, DateTime? dateEffective)
        {
    
            //List<ResourceType> resourceTypes = resourceTypeRepository.GetAllResourceTypes();
            string pCompanyID = (companyID ?? "B68162");
            DateTime pDateEffective = dateEffective ?? DateTime.Now;
            //pDateEffective = DateTime.Parse("2018-05-03");
            List<ResourceAssignmentOverViewViewModel> viewModels = new List<ResourceAssignmentOverViewViewModel>();

            for (int i = 0; i<=5;i++)
            {
                viewModels.Add( new ResourceAssignmentOverViewViewModel()
                { 
                    CompanyID = pCompanyID,
                    DateEffective = pDateEffective.AddDays(i),
                    DateEffectiveBookingRoomStayResourceAssignments = resourceAssignmentRepository.GetDateEffectiveBookingItemResourceAssignments(pCompanyID, pDateEffective.AddDays(i)),
                    ResourceTypes = resourceTypeRepository.GetAllResourceTypes()
                }
                );
        
            }

            return View(viewModels);
        }

              
    }
}