using BookingEngineV1.Models.DBViews;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IShopRepository
    {


        //IEnumerable<ResourceType> ShopResourceTypes { get; }

        //IEnumerable<ShopDisplayAll> GetShopDisplayAllLines();

        Cart AddCartItemToCart(Cart cart, int resourceTypeID, int rateCompositionID, DateTime dateOfArrival, DateTime dateOfDeparture, int numberOfUnits, decimal pricePerUnit);
        List<CartItemDay> CreateCartItemDays(CartItem cartItem);
        Cart GetCart(long cartID);
        Cart CreateCart(Inquiry inquiry, string userID);
        CartItem GetCartItem(long cartItemID);
        Cart RemoveCartItemFromCart(CartItem cartItem);
        Cart UpdateCartItemInCart(CartItem cartItem, int rateCompositionID, int numberOfUnits, int numberOfGuests, decimal pricePerUnit);
        Inquiry CreateInquiry(string companyID, int channelID, DateTime dateOfArrival, DateTime dateOfDeparture, int numberOfUnits, int numberOfGuests);
        List<CartItemDayRateDetails> GetCartItemDayRateDetails(CartItem cartItem);
        List<RateCompositionItem> GetRateCompositionItems(int rateCompositionID);

        Booking CreateBookingAtCheckout(Cart cart);
        BookingRoomStay CreateBookingItem(Booking booking, CartItem cartItem);
        List<BookingRoomStayDay> CreateBookingItemDays(BookingRoomStay bookingItem, List<CartItemDay> cartItemDays);
        List<BookingItemDayPromotion> CreateBookingItemDayPromotions(BookingRoomStayDay bookingItemDay, List<CartItemDayPromotion> cartItemDayPromotions);

    }
}
