using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookingEngineV1.Data;
using BookingEngineV1.Models;
using BookingEngineV1.Services;
using BookingEngineV1.Models.Repositories;
using BookingEngineV1.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using jsreport.AspNetCore;
using jsreport.Binary;
using jsreport.Local;

namespace BookingEngineV1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJsReport(new LocalReporting().UseBinary(JsReportBinary.GetBinary()).AsUtility().Create());
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IResourceTypeRepository, ResourceTypeRepository>();
            services.AddTransient<IBookingRepository, BookingRepository>();
            services.AddTransient<IChannelRepository, ChannelRepository>();
            services.AddTransient<IRateCompositionRepository, RateCompositionRepository>();
            services.AddTransient<IShopRepository, ShopRepository>();
            services.AddTransient<IResourceStatusRepository, ResourceStatusRepository>();
            services.AddTransient<IBookingItemResourceAssignmentRepository, BookingItemResourceAssignmentRepository>();
            services.AddTransient<IAvailableResourceTypesRepository, AvailableResourceTypesRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            string conString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(options => options.UseSqlServer(conString));
            //services.AddDbContext<ManualContext>(options => options.UseSqlServer(conString));



            services.AddDistributedSqlServerCache(
                options =>
                {
                    options.ConnectionString = conString;
                    options.SchemaName = "dbo";
                    options.TableName = "SessionData";
                });
            services.AddSession(options =>
            {
                options.Cookie.Name = "BookingEngine.Session";
                options.IdleTimeout = System.TimeSpan.FromHours(48);
                options.Cookie.HttpOnly = false;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
