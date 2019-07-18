using BookingEngineV1.Models.DBViews;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.Interfaces;
using BookingEngineV1.Models.Manual;
using BookingEngineV1.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Repositories
{
    public class ShopRepository : IShopRepository
    {

        private DataContext context;
        public ShopRepository(DataContext ctx) => context = ctx;


        public Cart AddCartItemToCart(Cart cart, int resourceTypeID, int rateCompositionID, DateTime dateOfArrival, DateTime dateOfDeparture, int numberOfUnits, decimal pricePerUnit)
        {
            string resourceTypeName = context.ResourceTypes.Where(x => x.ResourceTypeID == resourceTypeID).SingleOrDefault().Name;
            CartItem ci = new CartItem()
            {
                CartID = cart.CartID,
                ResourceTypeID = resourceTypeID,
                RateCompositionID = rateCompositionID,
                DateOfArrival = dateOfArrival,
                DateOfDeparture = dateOfDeparture,
                NumberOfUnits = numberOfUnits,
                RateCompositionItems = GetRateCompositionItems(rateCompositionID)
            };

            context.CartItems.Add(ci);
            ci.CartItemDays = CreateCartItemDays(ci);
            context.SaveChanges();

            cart.CartItems = context.CartItems.Where(x => x.CartID == cart.CartID)
                .Include(y=>y.CartItemDays).ToList();
            context.SaveChanges();
            return cart;           
        }

        public Cart UpdateCartItemInCart(CartItem cartItem, int rateCompositionID, int numberOfUnits, int numberOfGuests, decimal pricePerUnit)
        {
            //CartItem ci = context.CartItems.Where(x => x.CartItemID == cartItem.CartItemID).SingleOrDefault();

            if(numberOfUnits == 0)
            {
                return RemoveCartItemFromCart(cartItem);
            }
            cartItem.NumberOfUnits = numberOfUnits;
            cartItem.RateCompositionID = rateCompositionID;
            cartItem.NumberOfGuests = numberOfGuests;
            context.CartItems.Update(cartItem);
            context.SaveChanges();
            Cart cart = context.Carts.Where(x => x.CartID == cartItem.CartID).SingleOrDefault();
            cart.CartItems = context.CartItems.Where(x => x.CartID == cart.CartID).ToList();
            return cart;
        }

        public Cart RemoveCartItemFromCart(CartItem cartItem)
        {
            Cart cart = GetCart(cartItem.CartID);
            context.CartItems.Remove(cartItem);
            context.SaveChanges();
            cart.CartItems = context.CartItems.Where(x => x.CartID == cart.CartID).ToList();
            return cart;
        }

        public List<CartItemDay> CreateCartItemDays(CartItem cartItem)
        {
            // GetTheRateDetails
           List<CartItemDayRateDetails> dbLines = GetCartItemDayRateDetails(cartItem);

            // SPLIT STAY INTO SINGLE DAYS
            for (DateTime dateEffective = cartItem.DateOfArrival; dateEffective <cartItem.DateOfDeparture; dateEffective = dateEffective.AddDays(1))
            {
                List<CartItemDayPromotion> cidPromotions = new List<CartItemDayPromotion>();
                CartItemDay newCartItemDay = new CartItemDay()
                {
                    CartItemID = cartItem.CartItemID,
                    DateEffective = dateEffective,
                    BaseOrDerivedPricePerUnit = dbLines.Where(x => x.DateEffective == dateEffective).SingleOrDefault().DerivedPrice
                };
                context.CartItemDays.Add(newCartItemDay);
                context.SaveChanges();

                // CALCULATE PROMOTIONS PER DAY
                CartItemDayPromotion lastMinutePromotion = new CartItemDayPromotion()
                {
                    CartItemDayID = newCartItemDay.CartItemDayID,
                    DateEffective = dateEffective,
                    CartItemDay = newCartItemDay,
                    PromotionTypeID = "PT02",
                    Amount = dbLines.Where(x => x.DateEffective == dateEffective).SingleOrDefault().LastMinutePromotion,
                };
                cidPromotions.Add(lastMinutePromotion);
                context.CartItemDayPromotions.Add(lastMinutePromotion);

                CartItemDayPromotion earlyBookingPromotion = new CartItemDayPromotion()
                {
                    CartItemDayID = newCartItemDay.CartItemDayID,
                    DateEffective = dateEffective,
                    CartItemDay = newCartItemDay,
                    PromotionTypeID = "PT01",
                    Amount = dbLines.Where(x => x.DateEffective == dateEffective).SingleOrDefault().EarlyBookingPromotion
                };
                cidPromotions.Add(earlyBookingPromotion);
                context.CartItemDayPromotions.Add(earlyBookingPromotion);

                CartItemDayPromotion longStayPromotion = new CartItemDayPromotion()
                {
                    CartItemDayID = newCartItemDay.CartItemDayID,
                    DateEffective = dateEffective,
                    CartItemDay = newCartItemDay,
                    PromotionTypeID = "PT03",
                    Amount = dbLines.Where(x => x.DateEffective == dateEffective).SingleOrDefault().LongStayPromotion
                };
                cidPromotions.Add(longStayPromotion);
                context.CartItemDayPromotions.Add(longStayPromotion);

                newCartItemDay.CartItemDayPromotions = cidPromotions;

                newCartItemDay.TotalPromotionPerUnit = newCartItemDay.CartItemDayPromotions.Sum(x=>x.Amount);
                context.SaveChanges();

            }
            context.SaveChanges();
            return context.CartItemDays.Where(x => x.CartItem == cartItem).ToList();
        }

        public List<RateCompositionItem> GetRateCompositionItems(int rateCompositionID)
        {
            List<RateCompositionItem> rcItems = new List<RateCompositionItem>();
            rcItems = context.RateCompositionItems.Where(cit => cit.RateCompositionID == rateCompositionID)
                .Include(rit => rit.Service).ToList();

            return rcItems;
        }

        public List<CartItemDayRateDetails> GetCartItemDayRateDetails(CartItem cartItem)
        {
            IQueryable<CartItemDayRateDetails> allDetails;

            string sDateOfBooking = cartItem.Cart.DateOfBooking.ToString("yyyy-MM-dd");
            string sDateOfArrival = cartItem.DateOfArrival.ToString("yyyy-MM-dd");
            string sDateOfDeparture = cartItem.DateOfDeparture.ToString("yyyy-MM-dd");

            SqlParameter pDateOfBooking = new SqlParameter("@DateOfBooking", sDateOfBooking);
            SqlParameter pDateOfArrival = new SqlParameter("@DateOfArrival", sDateOfArrival);
            SqlParameter pDateOfDeparture = new SqlParameter("@DateOfDeparture", sDateOfDeparture);
            SqlParameter pCompanyID = new SqlParameter("@CompanyID", cartItem.Cart.CompanyID);
            SqlParameter pResourceTypeID = new SqlParameter("@ResourceTypeID", cartItem.ResourceTypeID);
            SqlParameter pRateCompositionID = new SqlParameter("@RateCompositionID", cartItem.RateCompositionID);

            string sqlAllDetails = $@"SELECT * 
                From fnResourceTypeRateDetailsOnOffer({pDateOfBooking}, {pDateOfArrival}, {pDateOfDeparture}, {pCompanyID}) V
                Where ResourceTypeID={pResourceTypeID} AND RateCompositionID = {pRateCompositionID}             
                option(maxrecursion 0)";
            allDetails = context.CartItemDayRateDetailsAllLines.FromSql(sqlAllDetails, pDateOfBooking, pDateOfArrival, pDateOfDeparture, pCompanyID, pResourceTypeID, pRateCompositionID);

            return allDetails.ToList();
        }

 

        public Cart CreateCart(Inquiry inquiry, string userId)
        {
            Cart newCart = new Cart()
            {
                CompanyID = inquiry.CompanyID,
                ChannelID = inquiry.ChannelID,
                DateOfBooking = System.DateTime.Now,
                UserID = userId
            };

            context.Carts.Add(newCart);
            context.SaveChanges();

            return newCart;

        }


        public Cart GetCart(long cartID)
        {
            Cart cart = context.Carts
                .Where(x => x.CartID == cartID)
                .Include(x => x.CartItems)
                    .ThenInclude(cartItemDay => cartItemDay.CartItemDays)
                    .SingleOrDefault();
            if (cart != null)
            {
                foreach (CartItem ci in cart.CartItems)
                {
                    ci.ResourceType = context.ResourceTypes.Where(x => x.ResourceTypeID == ci.ResourceTypeID).SingleOrDefault();
                    ci.RateCompositionItems = context.RateCompositionItems.Where(rci => rci.RateCompositionID == ci.RateCompositionID)
                        .Include(a => a.Service).ToList();

                    foreach(CartItemDay cid in ci.CartItemDays)
                    {
                        cid.CartItemDayPromotions = context.CartItemDayPromotions.Where(x=>x.CartItemDayID == cid.CartItemDayID).ToList();
                    }
                    
                }
            }
            
            return cart;
        }

        public CartItem GetCartItem(long cartItemID)
        {
            CartItem cartItem = context.CartItems.Where(x => x.CartItemID == cartItemID).SingleOrDefault();
            return cartItem;
        }

        public Inquiry CreateInquiry(string companyID, int channelID, DateTime dateOfArrival, DateTime dateOfDeparture, int numberOfUnits, int numberOfGuests)
        {
            Inquiry inq = new Inquiry()
            {
                CompanyID = companyID,
                ChannelID = channelID,
                DateOfArrival = dateOfArrival,
                DateOfDeparture = dateOfDeparture,
                NumberOfUnits = numberOfUnits,
                NumberOfGuests = numberOfGuests
            };

            context.Inquiries.Add(inq);
            context.SaveChanges();

            return inq;
        }

        public Booking CreateBookingAtCheckout(Cart cart)
        {
            Booking newBooking = new Booking()
            {

                ChannelID = cart.ChannelID,
                CompanyID = cart.CompanyID,
                DateOfBooking = cart.DateOfBooking,
                //DateOfArrival = cart.DateOfArrival,
                //DateOfDeparture = cart.DateOfDeparture,
                UserID = cart.UserID,
                Status = "From Cart"
            };

            context.Bookings.Add(newBooking);
            context.SaveChanges();

            List<BookingRoomStay> bookingRoomStays = new List<BookingRoomStay>();

            foreach (CartItem ci in cart.CartItems)
            {
                BookingRoomStay newBI = new BookingRoomStay();
                newBI = CreateBookingItem(newBooking, ci);
                context.BookingRoomStays.Add(newBI);
                context.SaveChanges();

                newBI.BookingRoomStayDays = CreateBookingItemDays(newBI, ci.CartItemDays);
                //context.BookingItemDays.Add(newBI.BookingItemDays);
                bookingRoomStays.Add(newBI);

            }

            newBooking.BookingRoomStays = bookingRoomStays;
            return newBooking;
        }

        public BookingRoomStay CreateBookingItem(Booking booking, CartItem cartItem)
        {
            BookingRoomStay newBookingItem = new BookingRoomStay()
            {
                BookingIDPMS = booking.BookingIDPMS,
                ResourceTypeId = cartItem.ResourceTypeID,
                RateCompositionId = cartItem.RateCompositionID,
                DateOfArrival = cartItem.DateOfArrival,
                DateOfDeparture = cartItem.DateOfDeparture,
                NumberOfUnits = cartItem.NumberOfUnits,
                NumberOfGuests = cartItem.NumberOfGuests
            };


            return newBookingItem;
        }

        public List<BookingRoomStayDay> CreateBookingItemDays(BookingRoomStay bookingRoomStay, List<CartItemDay> cartItemDays)
        {
            List<BookingRoomStayDay> biDays = new List<BookingRoomStayDay>();

            foreach (CartItemDay cid in cartItemDays)
            {
                BookingRoomStayDay newBookingItemDay = new BookingRoomStayDay()
                {
                    BookingRoomStayID = bookingRoomStay.BookingRoomStayId,
                    DateEffective = cid.DateEffective,
                    BaseOrDerivedPricePerUnit = cid.BaseOrDerivedPricePerUnit,
                    TotalPromotionPerUnit = cid.TotalPromotionPerUnit,
                    PriceBeforeTaxPerUnit = cid.PriceBeforeTaxPerUnit,
                    VATPercentage = cid.VATPercentage,
                    VATPerUnit = cid.VATPerUnit,
                    PriceAfterTaxPerUnit = cid.PriceAfterTaxPerUnit
                };
                context.BookingRoomStayDays.Add(newBookingItemDay);
                context.SaveChanges();

                newBookingItemDay.BookingItemDayPromotions = CreateBookingItemDayPromotions(newBookingItemDay, cid.CartItemDayPromotions);

                context.BookingRoomStayDays.Update(newBookingItemDay);
                context.SaveChanges();
                biDays.Add(newBookingItemDay);
            }

            return biDays;
        }

        public List<BookingItemDayPromotion> CreateBookingItemDayPromotions(BookingRoomStayDay bookingItemDay, List<CartItemDayPromotion> cartItemDayPromotions)
        {
            List<BookingItemDayPromotion> proms = new List<BookingItemDayPromotion>();

            if (cartItemDayPromotions != null)
            {

                foreach (CartItemDayPromotion cidp in cartItemDayPromotions)
                {
                    BookingItemDayPromotion newBookingItemDayPromotion = new BookingItemDayPromotion()
                    {
                        BookingRoomStayDayID = bookingItemDay.BookingRoomStayDayID,
                        PromotionTypeID = cidp.PromotionTypeID,
                        Amount = cidp.Amount
                    };

                    context.BookingItemDayPromotions.Add(newBookingItemDayPromotion);
                    context.SaveChanges();
                    proms.Add(newBookingItemDayPromotion);
                }
            }

            return proms;
        }
    }
}
