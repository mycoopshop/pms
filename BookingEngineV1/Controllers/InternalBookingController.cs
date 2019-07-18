using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookingEngine.Controllers
{
    public class InternalBookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}