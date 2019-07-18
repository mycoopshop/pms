using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingEngineV1.Controllers
{
    public class EditBookingController : Controller
    {
        private readonly IBookingRepository bookingRepository;

        public IActionResult Index()
        {
            return View();
        }
    }
}