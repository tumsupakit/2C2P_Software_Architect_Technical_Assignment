using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Transaction.BusinessLogic.Helpers;
using Transaction.BusinessLogic.Interfaces;
using Transaction.BusinessLogic.ViewModels;

namespace Transaction.BusinessLogic
{
    public class CsvValidator : ICsvValidator
    {
        public List<string> Validate(CsvTransactionModel transaction)
        {
            var errorList = new List<string>();
            string errorFormat = "Transaction Id {0} : {1}";
            var transactionStatus = new List<string> { "Approved", "Failed", "Finished" };

            string transactionId = transaction.TransactionId;

            if (transaction.TransactionId.Length > 50)
                errorList.Add(string.Format(errorFormat, transactionId, "Transaction ID length should be less than 50"));

            if (!decimal.TryParse(transaction.Amount, out _))
                errorList.Add(string.Format(errorFormat, transactionId, "Amount should be decimal number"));

            if (!CurrencyCodeHelper.IsCurrencyFormatCorrect(transaction.CurrencyCode))
                errorList.Add(string.Format(errorFormat, transactionId, "Currency Code is not correct"));

            if (!DateTime.TryParseExact(transaction.TransactionDate, "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out _))
                errorList.Add(string.Format(errorFormat, transactionId, "Transaction Date format is not correct"));

            if (!transactionStatus.Contains(transaction.Status))
                errorList.Add(string.Format(errorFormat, transactionId, "Transaction Status is not correct"));

            return errorList;
        }
    }
}
