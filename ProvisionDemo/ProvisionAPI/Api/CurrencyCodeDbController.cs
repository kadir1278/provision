using Microsoft.AspNetCore.Mvc;
using ProvisionBusinessLayer.Service;
using ProvisionDataLayer.Context;
using ProvisionDataLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ProvisionAPI.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyCodeDbController : Controller
    {
        private readonly SystemContext db;

        public CurrencyCodeDbController(SystemContext db)
        {
            this.db = db;
        }
        [HttpPost("add")]
        public IActionResult CurrencyCodeDb()
        {
            string msg = CurrencyCodeService.Add(db);
            return Ok(msg);
        }


        [HttpGet("list")]
        public List<CurrencyCode> LastTwoMonthDataList()
        {
            var list = db.CurrencyCodes.ToList();
            return list;
        }
    }
}
