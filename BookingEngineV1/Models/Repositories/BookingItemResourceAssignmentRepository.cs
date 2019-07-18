using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class BookingItemResourceAssignmentRepository : IBookingItemResourceAssignmentRepository
    {
        private readonly DataContext context;
        public BookingItemResourceAssignmentRepository(DataContext ctx) => context = ctx;

        public List<BookingRoomStay> GetBookingItemsByArrivalDate(string companyID, DateTime dateOfArrival)
        {
            List<BookingRoomStay> aBI = new List<BookingRoomStay>();
            aBI = context.BookingRoomStays
                .Where(x => x.DateOfArrival == dateOfArrival)
                .Include(x=>x.ResourceType)
                .Include(y=>y.RateComposition)
                .Include(a=>a.BookingRoomStayResourceAssignments).ThenInclude(a=>a.Resource)
                .Include(b=>b.BookingRoomStayDays)
                .Include(c=>c.Booking)
                .Where(x=>x.NumberOfUnits!=0)
                .ToList();

            foreach(BookingRoomStay bi in aBI)
            {
                bi.ResourcesAvailableForAssignment = GetResourcesAvailableForAssignment(bi);
            }

            return aBI;
        }

        public List<BookingRoomStayResourceAssignment> GetBookingItemResourceAssignments(string companyID, DateTime dateOfArrival)
        {
            List<BookingRoomStayResourceAssignment> biRAs = new List<BookingRoomStayResourceAssignment>();
            context.BookingRoomStayResourceAssignments
                .Include(x => x.Resource).ToList();
   
                
            return biRAs;
        }

        public List<Resource> GetResourcesAvailableForAssignment(BookingRoomStay bookingItem)
        {
            IQueryable<Resource> queryResourceLines;
            List<Resource> resourcesAvailable = new List<Resource>();
            

            //DateTime sDateOfArrival = dateOfArrival.ToString("yyyy-MM-dd");
            SqlParameter pDateOfArrival = new SqlParameter("@DateOfArrival", bookingItem.DateOfArrival);
            SqlParameter pDateOfDeparture = new SqlParameter("@DateOfDeparture", bookingItem.DateOfDeparture);
            SqlParameter pCompanyID = new SqlParameter("@CompanyID", bookingItem.Booking.CompanyID);
            string sqlResourcesAvailable = $@"select ResourceID, Name, CompanyID, ResourceTypeID, BedType, CapacityMax, Floor, SizeInM2, Description
                    from fnResourceAvailableForAssignment({pDateOfArrival},{pDateOfDeparture},{pCompanyID})";
            //RA
            //                    inner join tBookingItem BI on RA.bookingRoomStayID = BI.bookingRoomStayID
            //                    Where (BI.DateOfArrival <= '{dateOfArrival}' and BI.DateOfDeparture >= DateAdd(day, 1, '{dateOfArrival}'))  and CompanyID = {pCompanyID})";

            queryResourceLines = context.Resources.FromSql(sqlResourcesAvailable, pDateOfArrival, pDateOfDeparture, pCompanyID);
            queryResourceLines = queryResourceLines.Include(x => x.ResourceType).Where(y => y.ResourceTypeID == bookingItem.ResourceTypeId);
            return resourcesAvailable = queryResourceLines.ToList();
        }


        public List<BookingRoomStayResourceAssignment> AssignResourceToBookingItem(int resourceID, long bookingRoomStayID, string comment)
        {
            BookingRoomStayResourceAssignment biRA = new BookingRoomStayResourceAssignment()
            {
                BookingRoomStayID = bookingRoomStayID,
                ResourceID = resourceID,
                Comment = comment
            };

            context.BookingRoomStayResourceAssignments.Add(biRA);
            context.SaveChanges();

            return context.BookingRoomStayResourceAssignments.Include(x=>x.Resource).ToList();
        }

        public void RemoveBookignItemResourceAssignement(BookingRoomStayResourceAssignment bookingRoomStayResourceAssignment)
        {
            BookingRoomStayResourceAssignment ra = new BookingRoomStayResourceAssignment();
            ra = context.BookingRoomStayResourceAssignments.Where(x => x.BookingRoomStayResourceAssignmentID == bookingRoomStayResourceAssignment.BookingRoomStayResourceAssignmentID).SingleOrDefault();
            context.BookingRoomStayResourceAssignments.Remove(ra);
            context.SaveChanges();
        }

        public List<BookingRoomStayResourceAssignment> GetDateEffectiveBookingItemResourceAssignments(string companyID, DateTime dateEffective)
        {

            IQueryable<BookingRoomStayResourceAssignment> dateEffectiveResourceAssignments;
            //= new IQueryable<BookingItemResourceAssignment>();

            SqlParameter pDateEffective1 = new  SqlParameter("@DateEffective", dateEffective);
            SqlParameter pCompanyID1 = new SqlParameter("@CompanyID", companyID);

            string sql = $@"select BookingItemResourceAssignmentID, bookingRoomStayID, ResourceID, Comment, BookingItemDayID
            from fnDateEffectiveBookingItemResourceAssignments({pCompanyID1}, {pDateEffective1})";
            dateEffectiveResourceAssignments = context.BookingRoomStayResourceAssignments.FromSql(sql, pCompanyID1, pDateEffective1);
            pDateEffective1 = null;
            pCompanyID1 = null;
            return dateEffectiveResourceAssignments.Include(x => x.BookingRoomStay).ThenInclude(b => b.Booking).ToList();
            //        .ThenInclude(BookingItem => BookingItem.BookingItemDays.Select(b=>b.DateEffective==dateEffective).FirstOrDefault()).ToList();
            //return dateEffectiveResourceAssignments;
        }

    }
}
