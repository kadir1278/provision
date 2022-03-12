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
        [HttpPost("add")]
        public IActionResult CurrencyCodeDb()
        {
            return Ok(CurrencyCodeService.Add());
        }


        [HttpGet("list")]
        public List<CurrencyCode> LastTwoMonthDataList()
        {
            return CurrencyCodeService.List();
        }
    }
}
