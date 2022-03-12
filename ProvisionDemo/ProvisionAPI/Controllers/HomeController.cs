using Microsoft.AspNetCore.Mvc;
using ProvisionDataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvisionAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly SystemContext db;

        public HomeController(SystemContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var data=db.CurrencyCodes.ToList();
            return View(data);
        }
    }
}
