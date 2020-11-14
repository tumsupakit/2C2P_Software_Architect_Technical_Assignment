using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.BusinessLogic.Interfaces;
using Transaction.BusinessLogic.ViewModels;

namespace Transaction.Web.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        public IEnumerable<TransactionViewModel> Get(TransactionFilter filter)
        {
            var transactions = transactionService.Filter(filter);

            return transactions;
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post() 
        {
            IFormFile file = Request.Form.Files[0];

            return Ok();
        }
    }
}
