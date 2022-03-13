using Microsoft.AspNetCore.Mvc;
using ProvisionBusinessLayer.Service;
using ProvisionDataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisionAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(CurrencyCodeService.List());
        }
    }
}
