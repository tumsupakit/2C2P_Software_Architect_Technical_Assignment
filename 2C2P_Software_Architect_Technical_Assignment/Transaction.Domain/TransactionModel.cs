using System;
using System.Collections.Generic;
using System.Linq;
using Transaction.Domain.Enum;

namespace Transaction.Domain
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
