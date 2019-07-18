using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models;
using BookingEngineV1.Models.DBViews;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingEngineV1.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private IShopRepository shopRepository;
        private IAvailableResourceTypesRepository availabilityRepsository;
        //private IBookingRepository bookingRepository;

        public ShopController(IShopRepository shRepo, IAvailableResourceTypesRepository aRtRepo)//, IBookingRepository bookingRepo)
        {
            shopRepository = shRepo;
            availabilityRepsository = aRtRepo;
            //bookingRepository = bookingRepo;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CheckAvailability(string companyID, int channelID, DateTime dateOfArrival, DateTime dateOfDeparture, int numberOfGuests, int numberOfUnits)
        {
           Inquiry  inquiry = shopRepository.CreateInquiry(companyID, channelID, dateOfArrival, dateOfDeparture, numberOfUnits, numberOfGuests);

            ShopDisplay shopDisplay = new ShopDisplay()
            {
                Inquiry = inquiry,
                OfferedResourceTypes = availabilityRepsository.GetOfferedResourceTypes(inquiry)
            };
            return View("Index", shopDisplay);
        }


        [HttpPost]
        public IActionResult ProcessShopSelection(Inquiry inquiry, long cartID, long cartItemID, int resourceTypeID, int rateCompositionID, int numberOfUnits, int numberOfGuests, decimal pricePerUnit )
        {
            inquiry.DateOfInquiry = System.DateTime.Now;
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


            ShopDisplay shop = new ShopDisplay()
            {
                Cart = cCart,
                OfferedResourceTypes = availabilityRepsository.GetOfferedResourceTypes(inquiry),
                Inquiry = inquiry,

            };

            return View(nameof(Index), shop);
        }

        [HttpPost]
        public IActionResult RemoveItemFromCart(long cartItemID, Inquiry inquiry)
        {
            CartItem cartItem = shopRepository.GetCartItem(cartItemID);
            Cart cart = shopRepository.GetCart(cartItem.CartID);


            ShopDisplay shop = new ShopDisplay()
            {
                Cart = cart,
                OfferedResourceTypes = availabilityRepsository.GetOfferedResourceTypes(inquiry),
                Inquiry = inquiry
            };

            return View(nameof(Index), shop);

        }

        [HttpGet]
        public IActionResult ProceedToReservation(int cartID)
        {
            Cart cart = shopRepository.GetCart(cartID);

            return View("Checkout", cart);
        }


        [HttpPost]
        public IActionResult Checkout(long cartID)
        {
            Cart c = shopRepository.GetCart(cartID);
            Booking b = shopRepository.CreateBookingAtCheckout(c);

            return View();
        }


    }
}
