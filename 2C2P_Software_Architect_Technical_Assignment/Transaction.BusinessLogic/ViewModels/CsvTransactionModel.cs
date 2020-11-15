using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.BusinessLogic.ViewModels
{
    public class CsvTransactionModel
    {
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }
    }
}
