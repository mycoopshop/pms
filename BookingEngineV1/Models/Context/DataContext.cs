using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models.DBViews;
using BookingEngineV1.Models.Entities;
using BookingEngineV1.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookingEngineV1.Models.DBQueries;

namespace BookingEngineV1.Models
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<ResourceDemandGroup> ResourceDemandGroups { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingRoomStay> BookingRoomStays { get; set; }
        public DbSet<BookingRoomStayDay> BookingRoomStayDays { get; set; }
        public DbSet<BookingItemDayPromotion> BookingItemDayPromotions { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<RateComposition> RateCompositions { get; set; }
        public DbSet<ResourceType> ShopResourceTypes { get; set; }
        public DbSet<ResourceStatusLog> ResourceStatusLogs { get; set; }
        public DbSet<ResourceStatus> ResourceStatuses { get;set; }
        public DbSet<CurrentResourceStatusViewModel> CurrentResourceStatuses { get; set; }
        public DbSet<ResourceStock> ResourceStocks { get; set; }
        public DbSet<ResourceBlock> ResourceBlocks { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartItemDay> CartItemDays { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<BookingRoomStayResourceAssignment> BookingRoomStayResourceAssignments { get; set; }

        public DbSet<ShopDisplayAll> ShopDisplayAllLines { get; set; }
        public DbSet<CreateBookingDisplayAll> CreateBookingDisplayAllLines { get; set; }
        public DbSet<CartItemDayRateDetails> CartItemDayRateDetailsAllLines { get; set; }
        public DbSet<CartItemDayPromotion> CartItemDayPromotions { get; set; }
        public DbSet<Service> RateItems { get; set; }

        public DbSet<RateCompositionItem> RateCompositionItems { get; set; }

        public DbSet<ResourceTypeUnitsAvailableForSale> ResourceTypeUnitsAvailableForSalesDateRange { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopDisplayAll>()
                .HasKey(o => new { o.ResourceRate_SK });

            modelBuilder.Entity<CreateBookingDisplayAll>()
                .HasKey(o => new { o.ResourceRate_SK });

            modelBuilder.Entity<CartItemDayRateDetails>()
                .HasKey(k => new { k.DateEffective });

            modelBuilder.Entity<ResourceTypeUnitsAvailableForSale>()
                .HasKey(d => new { d.DateEffective, d.ResourceTypeID });

     

        }
    }
}
