using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.ViewModels;
using Transaction.Domain;
using Transaction.Domain.Enum;

namespace Transaction.BusinessLogic.ModelMappers
{
    public static class XmlTransactionModelToTransactionModelMapper
    {
        public static IEnumerable<TransactionModel> MapToModel(this XmlTransactionModel item) 
        {
            var models = new List<TransactionModel>();
            var statusDictionary = new Dictionary<string, TransactionStatus>();
            statusDictionary.Add("Approved", TransactionStatus.Approved);
            statusDictionary.Add("Rejected", TransactionStatus.Rejected);
            statusDictionary.Add("Done", TransactionStatus.Done);

            foreach (XmlTransaction transaction in item.Transactions)
            {
                models.Add(new TransactionModel
                {
                    TransactionId = transaction.id,
                    Amount = decimal.Parse(transaction.PaymentDetails.Amount),
                    CurrencyCode = transaction.PaymentDetails.CurrencyCode,
                    TransactionDate = DateTime.Parse(transaction.TransactionDate),
                    Status = statusDictionary[transaction.Status]
                });
            }

            return models;
        }
    }
}
