using Microsoft.AspNetCore.Mvc;
using ProvisionBusinessLayer.Service;
using ProvisionBusinessLayer.ViewModels;
using ProvisionDataLayer.Context;
using ProvisionDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ProvisionAPI.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastTwoMonthDataDbController : ControllerBase
    {
        [HttpPost("add/{code}")]
        public IActionResult LastTwoMonthDataDb(string code)
        {
            string msg = TcmbDataService.Add(code);
            return Ok(msg);
        }
        [HttpGet("list/{code}")]
        public List<TCMBViewModel> LastTwoMonthDataList(string code)
        {
            return TcmbDataService.List(code);
        }
    }
}
