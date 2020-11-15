using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        public IActionResult Post()
        {
            IFormFile file = Request.Form.Files[0];
            IList<string> errorMessages = transactionService.Upload(file);

            if (errorMessages.Count > 0)
                return BadRequest(errorMessages);
            else
                return Ok();
        }
    }
}
