using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Domain;
using Transaction.Domain.Enum;

namespace Transaction.Persistence
{
    public interface ITransactionRepository
    {
        IEnumerable<TransactionModel> Filter(string currencyCode, DateTime? dateFrom, DateTime? dateTo, TransactionStatus? status);
        void AddRange(IEnumerable<TransactionModel> transactions);
    }
}
