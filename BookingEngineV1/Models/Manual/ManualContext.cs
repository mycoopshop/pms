using BookingEngineV1.Models.DBViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingEngineV1.Models.Manual
{
    public class ManualContext : DbContext
    {
        public ManualContext(DbContextOptions<ManualContext> options) : base(options) { }
        public DbSet<ShopDisplayAll> ShopDisplayAllLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopDisplayAll>().ToTable("ShopDisplayAllLines");
            modelBuilder.Entity<ShopDisplayAll>().HasKey(o => new {o.ResourceRate_SK});
            //modelBuilder.Entity<ShopDisplayAll>().Property(t => t.CompanyID).HasColumnName("CompanyID");
            //modelBuilder.Entity<OfferForDateSelection>().Property(t => t.ResourceTypeID).HasColumnName("ResourceTypeID");
            //modelBuilder.Entity<OfferForDateSelection>().Property(t => t.RateCompositionID).HasColumnName("RateCompositionID");

        }

    }
}

