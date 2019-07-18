using BookingEngineV1.Models.DBQueries;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class BookingRepository : IBookingRepository
    {

        private readonly DataContext context;
        public BookingRepository(DataContext ctx) => context = ctx;

    

        //    public IEnumerable<Booking> Bookings => context.Bookings.Include(ch=>ch.Channel).Include(bt => bt.BookingItems).ThenInclude(bi => bi.ResourceType);


        //    // *************** BOOKING *************************************

        public Booking GetBooking(long bookingIDPMS)
                => context.Bookings.Include(bt => bt.BookingRoomStays).First(bt
                   => bt.BookingIDPMS == bookingIDPMS);



        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *  
        public List<Booking> GetBookings(BookingsSelection selection)
        {
            DateTime dateMax = DateTime.Now.AddDays(720);
            DateTime dateMin = DateTime.Parse("1990-01-01");

            DateTime dateOfBookingFrom;
            DateTime dateOfBookingTo;

            switch (selection.DateOfBookingOperator)
            {
                case "=":              
                    dateOfBookingFrom = (selection.DateOfBooking == DateTime.MinValue ? dateMin : selection.DateOfBooking);
                    dateOfBookingTo = dateOfBookingFrom;
                    break;                   
                case ">=":
                    dateOfBookingFrom = (selection.DateOfBooking == DateTime.MinValue ? dateMin : selection.DateOfBooking);
                    dateOfBookingTo = dateMax;
                    break;

                case "<=":
                    dateOfBookingFrom = dateMin;
                    dateOfBookingTo = (selection.DateOfBooking == DateTime.MinValue ? dateMax : selection.DateOfBooking);
                    break;
                default:
                    dateOfBookingFrom = dateMin;
                    dateOfBookingTo = dateMax;
                    break;
            }

            DateTime dateOfArrivalFrom;
            DateTime dateOfArrivalTo;
            switch (selection.DateOfArrivalOperator)
            {
                case "=":
                    dateOfArrivalFrom = (selection.DateOfArrival == DateTime.MinValue ? dateMin : selection.DateOfArrival);
                    dateOfArrivalTo = dateOfArrivalFrom;
                    break;
                case ">=":
                    dateOfArrivalFrom = (selection.DateOfArrival == DateTime.MinValue ? dateMin : selection.DateOfArrival);
                    dateOfArrivalTo = dateMax;
                    break;

                case "<=":
                    dateOfArrivalFrom = dateMin;
                    dateOfArrivalTo = (selection.DateOfArrival == DateTime.MinValue ? dateMax : selection.DateOfArrival);
                    break;
                default:
                    dateOfArrivalFrom = dateMin;
                    dateOfArrivalTo = dateMax;
                    break;
            }

            DateTime dateOfDepartureFrom;
            DateTime dateOfDepartureTo;
            switch (selection.DateOfDepartureOperator)
            {
                case "=":
                    dateOfDepartureFrom = (selection.DateOfDeparture == DateTime.MinValue ? dateMin : selection.DateOfDeparture);
                    dateOfDepartureTo = dateOfDepartureFrom;
                    break;
                case ">=":
                    dateOfDepartureFrom = (selection.DateOfDeparture == DateTime.MinValue ? dateMin : selection.DateOfDeparture);
                    dateOfDepartureTo = dateMax;
                    break;

                case "<=":
                    dateOfDepartureFrom = dateMin;
                    dateOfDepartureTo = (selection.DateOfDeparture == DateTime.MinValue ? dateMax : selection.DateOfDeparture);
                    break;
                default:
                    dateOfDepartureFrom = dateMin;
                    dateOfDepartureTo = dateMax;
                    break;
            }


            string vChannelID = "null";
            if (selection.ChannelID != 0) { vChannelID = selection.ChannelID.ToString(); };

            string vRateCompositionID = "null";
            if(selection.RateCompositionID != 0) { vRateCompositionID = selection.RateCompositionID.ToString(); };
            var vResourceTypeID = "null";
            if(selection.ResourceTypeID != 0) { vResourceTypeID = selection.ResourceTypeID.ToString(); };


            var pCompanyID = "null";
            if (selection.CompanyID != null)
            {
                pCompanyID = $"'{selection.CompanyID}'";
            }
            var pGuestNames = "null";
            if (selection.GuestNames != null)
            {
                pGuestNames = $"'{selection.GuestNames}'";
            }
            var pChannelID = vChannelID;
            var pDateOfBookingFrom = dateOfBookingFrom.ToString("yyyy-MM-dd");
            var pDateOfBookingTo = dateOfBookingTo.ToString("yyyy-MM-dd");
            var pDateOfArrivalFrom = dateOfArrivalFrom.ToString("yyyy-MM-dd");
            var pDateOfArrivalTo = dateOfArrivalTo.ToString("yyyy-MM-dd");
            var pDateOfDepartureFrom = dateOfDepartureFrom.ToString("yyyy-MM-dd");
            var pDateOfDepartureTo = dateOfDepartureTo.ToString("yyyy-MM-dd");
            var pRateCompositionID = vRateCompositionID;
            var pResourceTypeID = vResourceTypeID;
       
            string sqlSearchBookings = $@"select *
                from fnSearchBookings({pCompanyID},{pChannelID},'{pDateOfBookingFrom}', '{pDateOfBookingTo}', '{pDateOfArrivalFrom}', '{pDateOfArrivalTo}',
                '{pDateOfDepartureFrom}', '{pDateOfDepartureTo}', {pRateCompositionID}, {pResourceTypeID}, {pGuestNames})";

           
            IQueryable<Booking> bookingsFound = context.Bookings;
            bookingsFound = context.Bookings.FromSql(sqlSearchBookings);

            bookingsFound = bookingsFound
                .Include(x => x.Channel)
                .Include(x => x.BookingRoomStays)
                    .ThenInclude(item => item.ResourceType)
                .Include(x => x.BookingRoomStays)

                    .ThenInclude(bookingRoomStay => bookingRoomStay.BookingRoomStayDays)
                .Include(x => x.BookingRoomStays)
                    .ThenInclude(bookingRoomStay => bookingRoomStay.RateComposition);


            return bookingsFound.AsNoTracking().ToList();

        }

        public List<Channel> GetChannels ()
        {
            return context.Channels.AsNoTracking().ToList();
        }

        public List<RateComposition>GetRateCompositions(string companyID)
        {
            return context.RateCompositions.Where(x => x.CompanyID == companyID).ToList();
        }

        public List<ResourceType> GetResourceTypes(string companyID)
        {
            List<ResourceType> resourceTypes = new List<ResourceType>();

            string pCompanyID = $@"'{companyID}'";

            string sqlRT = $@"select Distinct RT.ResourceTypeID 'ResourceTypeID',R.[Description], RDG.ResourceDemandGroupID, RT.Name as 'Name'
                    from tResource R
                    inner join tResourceType RT on R.ResourceTypeID = RT.ResourceTypeID
                    inner join tResourceDemandGroup RDG on RT.ResourceDemandGroupID = RDG.ResourceDemandGroupID
                    Where RDG.CompanyID = {pCompanyID};";

            resourceTypes = context.ResourceTypes.FromSql(sqlRT).AsNoTracking().ToList();
            return resourceTypes;
        }

        public BookingRoomStay GetBookingRoomStay(long bookingRoomStayId)
        {
            BookingRoomStay bookingRoomStay = context.BookingRoomStays.Where(x => x.BookingRoomStayId == bookingRoomStayId)
                .Include(x=>x.Booking)
                .SingleOrDefault();
            return bookingRoomStay;
        }

        public Booking GetBookingDetails(long bookingIDPMS)
        {
            Booking bookingDetails = context.Bookings.Where(x => x.BookingIDPMS == bookingIDPMS)
                 .Include(ch => ch.Channel)
                 .Include(a => a.BookingRoomStays)
                     .ThenInclude(b => b.BookingRoomStayDays)
                        .ThenInclude(b=>b.BookingItemDayPromotions)
                 .Include(a => a.BookingRoomStays)
                     .ThenInclude(rt => rt.ResourceType)
                 .Include(a => a.BookingRoomStays)
                     .ThenInclude(rc => rc.RateComposition).FirstOrDefault();

            return bookingDetails;
        }

        public Booking CreateBooking(string companyID, int channelID, string userID)
        {
            Booking newBooking = new Booking()
            {
                CompanyID = companyID,
                ChannelID = channelID,
                DateOfBooking = System.DateTime.Now
            };

            context.Bookings.Add(newBooking);
            context.SaveChanges();
            return newBooking;

        }
        public Booking AddBookingItem(Booking booking, int resourceTypeID, int rateCompositionID, int numberOfUnits, int numberOfGuests, string guestNames, DateTime dateOfArrival, DateTime dateOfDeparture)
        {
            BookingRoomStay newBookingItem = new BookingRoomStay()
            {
                BookingIDPMS = booking.BookingIDPMS,
                ResourceTypeId = resourceTypeID,
                RateCompositionId = rateCompositionID,
                DateOfArrival = dateOfArrival,
                DateOfDeparture = dateOfDeparture,
                NumberOfUnits = numberOfUnits,
                NumberOfGuests = numberOfGuests,
                GuestNames = guestNames,
            };

            context.BookingRoomStays.Add(newBookingItem);
            context.SaveChanges();

            newBookingItem.BookingRoomStayDays = AddBookingItemDays(newBookingItem);

            return GetBookingDetails(newBookingItem.BookingIDPMS);
        }

        public List<BookingRoomStayDay> AddBookingItemDays(BookingRoomStay bookingRoomStay)
        {
            List<BookingRoomStayDay> bIDS = new List<BookingRoomStayDay>();

            for (DateTime dateEffective = bookingRoomStay.DateOfArrival; dateEffective <= bookingRoomStay.DateOfDeparture; dateEffective = dateEffective.AddDays(1))
            {
                BookingRoomStayDay bID = new BookingRoomStayDay()
                {
                    DateEffective = dateEffective,
                    BookingRoomStayID = bookingRoomStay.BookingRoomStayId
                };
                context.BookingRoomStayDays.Add(bID);
                context.SaveChanges();
                bIDS.Add(bID);
            }
            return bIDS;
        }



        public BookingRoomStay IncreaseNumberOfUnits(BookingRoomStay bookingRoomStay, int newNumberOfUnits)
        {
            bookingRoomStay.NumberOfUnits = newNumberOfUnits;
            context.SaveChanges();
            return bookingRoomStay;

        }
        public BookingRoomStay DecreaseNumberOfUnits(BookingRoomStay bookingRoomStay, int newNumberOfUnits)
        {
            bookingRoomStay.NumberOfUnits = newNumberOfUnits;
            context.SaveChanges();
            return bookingRoomStay;
        }
        public BookingRoomStay UpgradeResourceType(BookingRoomStay bookingItem, int newResourceTypeID)
        {
            bookingItem.ResourceTypeId = newResourceTypeID;
            context.SaveChanges();
            return bookingItem;
        }
        public BookingRoomStay DowngradeResourceType(BookingRoomStay bookingItem, int newResourceTypeID)
        {
            bookingItem.ResourceTypeId = newResourceTypeID;
            context.SaveChanges();
            return bookingItem;
        }
        public BookingRoomStay ExtendStay(BookingRoomStay bookingItem, DateTime newDateOfArrival, DateTime newDateOfDeparture)
        {
            bookingItem.DateOfArrival = newDateOfDeparture;
            bookingItem.DateOfDeparture = newDateOfDeparture;
            context.SaveChanges();
            return bookingItem;
        }
        public BookingRoomStay ShortenStay(BookingRoomStay bookingItem, DateTime newDateOfArrival, DateTime newDateOfDeparture)
        {
            bookingItem.DateOfArrival = newDateOfDeparture;
            bookingItem.DateOfDeparture = newDateOfDeparture;
            context.SaveChanges();
            return bookingItem;
        }

        public Cart MapBookingToCart(long bookingIDPMS)
        {

            Booking booking = GetBookingDetails(bookingIDPMS);
            Cart c = new Cart()
            {
                CompanyID = booking.CompanyID,
                ChannelID = booking.ChannelID,
                DateOfBooking = booking.DateOfBooking,
                UserID = booking.UserID,
                Comment = booking.Comment
            };
            context.Carts.Add(c);
            context.SaveChanges();

            foreach (BookingRoomStay bi in booking.BookingRoomStays)
            {
                CartItem ci = MapBookingItemToCartItem(bi, c.CartID);
                //c.CartItems.Add(ci);

                foreach (BookingRoomStayDay bid in bi.BookingRoomStayDays)
                {
                    CartItemDay cid = MapBookingItemDayToCartItemDay(bid, ci.CartItemID);
                    //ci.CartItemDays.Add(cid);

                    foreach(BookingItemDayPromotion bidp in bid.BookingItemDayPromotions)
                    {
                        CartItemDayPromotion cidp = MapBookingItemDayPromotion(bidp, cid.CartItemDayID);
                        cid.CartItemDayPromotions.Add(cidp);
                    }
                }
            }

            return c;

        }

        public CartItemDay MapBookingItemDayToCartItemDay(BookingRoomStayDay bid, long cartItemID)
        {
            CartItemDay cid = new CartItemDay()
            {
                CartItemID = cartItemID,
                DateEffective = bid.DateEffective,
                BaseOrDerivedPricePerUnit = bid.BaseOrDerivedPricePerUnit,
                TotalPromotionPerUnit = bid.TotalPromotionPerUnit
            };
            context.CartItemDays.Add(cid);
            context.SaveChanges();
            return cid;
        }

        public CartItemDayPromotion MapBookingItemDayPromotion(BookingItemDayPromotion bidp, long cartItemDayID)
        {
            CartItemDayPromotion cidp = new CartItemDayPromotion()
            {
                CartItemDayID = cartItemDayID,
                PromotionTypeID = bidp.PromotionTypeID,
                Amount = bidp.Amount
            };
            context.CartItemDayPromotions.Add(cidp);
            context.SaveChanges();

            return cidp;
        }

        public CartItem MapBookingItemToCartItem(BookingRoomStay bi, long cartID)
        {
            CartItem ci = new CartItem()
            {
                CartID = cartID,
                ResourceTypeID = bi.ResourceTypeId,
                RateCompositionID = bi.RateCompositionId,
                DateOfArrival = bi.DateOfArrival,
                DateOfDeparture = bi.DateOfDeparture,
                NumberOfUnits = bi.NumberOfUnits,
                NumberOfGuests = bi.NumberOfGuests
            };
            context.CartItems.Add(ci);
            context.SaveChanges();

            return ci;
        }
             
    }

}

