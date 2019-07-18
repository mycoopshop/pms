using BookingEngineV1.Models.DBQueries;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IBookingRepository
    {

        List<Channel> GetChannels();
        List<RateComposition> GetRateCompositions(string companyID);
        List<ResourceType> GetResourceTypes(string companyID);
        Booking GetBookingDetails(long bookingIDPMS);
        BookingRoomStay GetBookingRoomStay(long bookingRoomStayId);

        List<Booking> GetBookings(BookingsSelection selection);

        Booking CreateBooking(string companyID, int channelID, string userID);
        Booking AddBookingItem(Booking booking, int resourceTypeID, int rateCompositionID, int numberOfUnits, int numberOfGuests, string guestNames, DateTime dateOfArrival, DateTime dateOfDeparture);
        List<BookingRoomStayDay> AddBookingItemDays(BookingRoomStay bookingItem);

        BookingRoomStay IncreaseNumberOfUnits(BookingRoomStay bookingItem, int newNumberOfUnits);
        BookingRoomStay DecreaseNumberOfUnits(BookingRoomStay bookingItem, int newNumberOfUnits);
        BookingRoomStay UpgradeResourceType(BookingRoomStay bookingItem, int newResourceTypeID);
        BookingRoomStay DowngradeResourceType(BookingRoomStay bookingItem, int newResourceTypeID);
        BookingRoomStay ExtendStay(BookingRoomStay bookingItem, DateTime newDateOfArrival, DateTime newDateOfDeparture);
        BookingRoomStay ShortenStay(BookingRoomStay bookingItem, DateTime newDateOfArrival, DateTime newDateOfDeparture);
        Cart MapBookingToCart(long bookingIDPMS);
        CartItem MapBookingItemToCartItem(BookingRoomStay bi, long cartID);
        CartItemDay MapBookingItemDayToCartItemDay(BookingRoomStayDay bid, long cartItemID);
    }
}
