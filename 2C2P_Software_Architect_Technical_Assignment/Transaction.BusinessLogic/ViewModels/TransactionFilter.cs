using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain.Enum;

namespace Transaction.BusinessLogic.ViewModels
{
    public class TransactionFilter
    {
        public string currencyCode { get; set; }
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
        public TransactionStatus? status {get;set; }
    }
}
