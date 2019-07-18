using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingEngineV1.Infrastructure;
using BookingEngineV1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BookingEngineV1.Controllers
{
    [ViewComponent(Name = "Cart")]
    public class CartController : Controller
    {
    }
}