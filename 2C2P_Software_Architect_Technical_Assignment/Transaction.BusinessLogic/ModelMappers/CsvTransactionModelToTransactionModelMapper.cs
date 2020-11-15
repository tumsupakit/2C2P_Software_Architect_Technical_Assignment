using System;
using System.Collections.Generic;
using System.Globalization;
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
            var statusDictionary = new Dictionary<string, TransactionStatus>();
            statusDictionary.Add("Approved", TransactionStatus.Approved);
            statusDictionary.Add("Failed", TransactionStatus.Rejected);
            statusDictionary.Add("Finished", TransactionStatus.Done);

            return new TransactionModel
            {
                TransactionId = item.TransactionId,
                Amount = decimal.Parse(item.Amount),
                CurrencyCode = item.CurrencyCode,
                TransactionDate = DateTime.ParseExact(item.TransactionDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                Status = statusDictionary[item.Status]
            };
        }
    }
}
