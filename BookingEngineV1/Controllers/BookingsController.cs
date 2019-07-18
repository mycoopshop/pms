using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//C:\Koppenberg\Github\PMS\BookingEngineV1\package.json
namespace BookingEngineV1.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IShopRepository shopRepository;
        private IAvailableResourceTypesRepository availabilityRepsository;

        public BookingsController(IBookingRepository bookingRepo, IAvailableResourceTypesRepository aRtRepo, IShopRepository shopRepo)
        {
            bookingRepository = bookingRepo;
            availabilityRepsository = aRtRepo;
            shopRepository = shopRepo;
        }

        //public IActionResult Index()
        //{
        //    BookingsSelection selection = new BookingsSelection();
        //    BookingViewModel bVM = new BookingViewModel()
        //    {
        //        Bookings = bookingRepository.GetBookings(selection)
        //    };
        //    return View(bVM);
        //}

        public IActionResult Index(BookingsSelection selection)

        {
            BookingViewModel bVM = new BookingViewModel()
            {
                BookingSelection = selection,
                Bookings = bookingRepository.GetBookings(selection),
                Channels = bookingRepository.GetChannels(),
                ResourceTypes = bookingRepository.GetResourceTypes(selection.CompanyID),
                RateCompositions = bookingRepository.GetRateCompositions(selection.CompanyID)
            };

            return View(bVM);

        }

        [HttpGet]
        public IActionResult ArrivalForm(long bookingRoomStayId)
        {
            BookingRoomStay brs = new BookingRoomStay();
            brs = bookingRepository.GetBookingRoomStay(bookingRoomStayId);
            ArrivalFormViewModel VM = new ArrivalFormViewModel()
            {
                BookingRoomStay = brs,
                Booking = brs.Booking
            };
            return View(VM);
        }

        public IActionResult ProcessBookingSelection(Inquiry inquiry, long cartID, long cartItemID, int resourceTypeID, int rateCompositionID, int numberOfUnits, int numberOfGuests, decimal pricePerUnit)
        {
            inquiry.DateOfInquiry = System.DateTime.Now;
            inquiry.ChannelID = 100;
            string userID = "system";
            Cart cCart = (shopRepository.GetCart(cartID) ?? shopRepository.CreateCart(inquiry, userID));

            CartItem cCartItem = shopRepository.GetCartItem(cartItemID);
            if (cCartItem == null)
            {
                cCart = shopRepository.AddCartItemToCart(cCart, resourceTypeID, rateCompositionID, inquiry.DateOfArrival, inquiry.DateOfDeparture, numberOfUnits, pricePerUnit);
            }
            else
            {
                cCart = shopRepository.UpdateCartItemInCart(cCartItem, rateCompositionID, numberOfUnits, numberOfGuests, pricePerUnit);
            }

            CreateBookingViewModel cBVM = new CreateBookingViewModel()
            {
                Inquiry = inquiry,
                Cart = cCart,
                Channels = bookingRepository.GetChannels(),
                OfferedResourceTypes = availabilityRepsository.GetOfferedResourceTypes(inquiry),
                ResourceTypes = bookingRepository.GetResourceTypes(inquiry.CompanyID),
                RateCompositions = bookingRepository.GetRateCompositions(inquiry.CompanyID)
            };

            return View(nameof(CreateBooking), cBVM);

        }

        public IActionResult GetBookingDetails(long bookingIDPMS)
        {
            Booking bookingDetails = bookingRepository.GetBookingDetails(bookingIDPMS);
            Cart cartDetails = bookingRepository.MapBookingToCart(bookingIDPMS);
            return View("BookingDetails", cartDetails);
            //return View();
        }

        public IActionResult EditBooking(long bookingIDPMS)
        {
            Cart cartForEdit = new Cart();
            cartForEdit = bookingRepository.MapBookingToCart(bookingIDPMS);

            EditBookingViewModel eBVM = new EditBookingViewModel()
            {
                Cart = cartForEdit,
                ResourceTypes = bookingRepository.GetResourceTypes(cartForEdit.CompanyID),
                RateCompositions = bookingRepository.GetRateCompositions(cartForEdit.CompanyID)
            };
            return View(eBVM);
        }

        [HttpPost]
        public IActionResult UpdateBookingItem(BookingRoomStay bookingItem)
        {

            return RedirectToAction(nameof(EditBooking), bookingItem.BookingIDPMS);
        }

        public IActionResult CreateBooking()
        {
            Inquiry inquiryDefault = new Inquiry()
            {
                CompanyID = "B68162",
                DateOfArrival = System.DateTime.Now,
                DateOfDeparture = System.DateTime.Now.AddDays(1)

            };

            CreateBookingViewModel cBVM = new CreateBookingViewModel()
            {
                Inquiry = inquiryDefault,
                ResourceTypes = bookingRepository.GetResourceTypes(inquiryDefault.CompanyID),
                Channels = bookingRepository.GetChannels(),
                RateCompositions = bookingRepository.GetRateCompositions(inquiryDefault.CompanyID),
            };

            return View(cBVM);
        }

        //[HttpPost]
        //public IActionResult CreateNewBooking(string companyID, int channelID, int resourceTypeID, int rateCompositionID,
        //    DateTime dateOfArrival, DateTime dateOfDeparture, int numberOfUnits, int numberOfGuests, string guestNames, string userID)
        //{
        //    string sCompanyID = companyID ?? "B68162";

        //    Cart newCart = new Cart();
        //    newCart = shopRepository.CreateCart(inquiry, userID);

        //    newBooking = bookingRepository.AddBookingItem(newBooking, resourceTypeID, rateCompositionID, numberOfUnits, numberOfGuests, guestNames, dateOfArrival, dateOfDeparture);

        //    CreateBookingViewModel cBVM = new CreateBookingViewModel()
        //    {
        //        Booking = newBooking,
        //        ResourceTypes = bookingRepository.GetResourceTypes(companyID),
        //        Channels = bookingRepository.GetChannels(),
        //        RateCompositions = bookingRepository.GetRateCompositions(companyID),
        //    };
        //    return View(nameof(CreateBooking), cBVM);
        //}

        public IActionResult GetAvailabilities(string companyID, DateTime dateOfArrival, DateTime dateOfDeparture)
        {
            Inquiry inquiry = new Inquiry()
            {
                CompanyID = "B68162",
                DateOfArrival = dateOfArrival,
                DateOfDeparture = dateOfDeparture,
                DateOfInquiry = DateTime.Now
            };

            CreateBookingViewModel cBVM = new CreateBookingViewModel()
            {
                Inquiry = inquiry,
                ResourceTypeUnitsAvailableForSale = availabilityRepsository.GetResourceTypesNumberOfUnitsAvailable(inquiry),
                OfferedResourceTypes = availabilityRepsository.GetOfferedResourceTypes(inquiry)
            };

            return View(nameof(CreateBooking), cBVM);

        }

        [HttpGet]
        public IActionResult ProceedToCheckout(long cartID)
        {
            Cart c = shopRepository.GetCart(cartID);

            return View("CheckoutBooking", c);

        }

        [HttpPost]
        public IActionResult Checkout(long cartID)
        {
            Cart c = shopRepository.GetCart(cartID);
            Booking b = shopRepository.CreateBookingAtCheckout(c);

            BookingsSelection bs = new BookingsSelection()
            {
                DateOfBooking = c.DateOfBooking
            };
            return RedirectToAction(nameof(Index), bs);
        }

    }
}