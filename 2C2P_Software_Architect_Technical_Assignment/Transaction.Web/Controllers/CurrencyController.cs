using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transaction.BusinessLogic.Interfaces;

namespace Transaction.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        ICurrencyService CurrencyService;

        public CurrencyController(ICurrencyService CurrencyService)
        {
            this.CurrencyService = CurrencyService;
        }

        [HttpGet]
        public string[] Get() 
        {
            return CurrencyService.Get();
        }
    }
}
