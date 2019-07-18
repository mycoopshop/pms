using BookingEngineV1.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Interfaces
{
    public interface IBookingItemResourceAssignmentRepository
    {
        List<BookingRoomStayResourceAssignment> AssignResourceToBookingItem(int resourceID, long bookingRoomStayID, string comment);
        List<BookingRoomStayResourceAssignment> GetBookingItemResourceAssignments(string companyID, DateTime dateOfArrival);
        void RemoveBookignItemResourceAssignement(BookingRoomStayResourceAssignment bookingItemResourceAssignment);
        List<Resource> GetResourcesAvailableForAssignment(BookingRoomStay bookingItem);
        List<BookingRoomStay> GetBookingItemsByArrivalDate(string companyID, DateTime dateOfArrival);
        List<BookingRoomStayResourceAssignment> GetDateEffectiveBookingItemResourceAssignments(string companyID, DateTime dateEffective);
    }
}
