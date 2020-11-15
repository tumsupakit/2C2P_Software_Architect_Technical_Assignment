using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.ViewModels;
using Transaction.Domain;
using Transaction.Domain.Enum;

namespace Transaction.BusinessLogic.ModelMappers
{
    public static class CsvTransactionModelToTransactionModelMapper
    {
        public static TransactionModel MapToModel(this CsvTransactionModel item) 
        {
            var transactionStatus = new Dictionary<string, TransactionStatus>();
            transactionStatus.Add("Approved", TransactionStatus.Approved);
            transactionStatus.Add("Failed", TransactionStatus.Rejected);
            transactionStatus.Add("Finished", TransactionStatus.Done);

            return new TransactionModel
            {
                TransactionId = item.TransactionId,
                Amount = decimal.Parse(item.Amount),
                CurrencyCode = item.CurrencyCode,
                TransactionDate = DateTime.Parse(item.TransactionDate),
                
            };
        }
    }
}
