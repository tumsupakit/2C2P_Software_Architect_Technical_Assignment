using System;
using System.Collections.Generic;
using Transaction.BusinessLogic.ViewModels;
using System.Text;
using Transaction.BusinessLogic.Interfaces;
using System.Globalization;
using Transaction.BusinessLogic.Helpers;

namespace Transaction.BusinessLogic
{
    public class XmlValidator : IXmlValidator
    {
        public List<string> Validate(XmlTransaction transaction)
        {
            var errorList = new List<string>();
            string errorFormat = "Transaction Id {0} : {1}";
            var transactionStatus = new List<string> { "Approved", "Rejected", "Done" };

            string transactionId = transaction.id;

            if (transaction.id.Length > 50)
                errorList.Add(string.Format(errorFormat, transactionId, "Transaction Id length should be less than 50"));

            if (!decimal.TryParse(transaction.PaymentDetails.Amount, out _))
                errorList.Add(string.Format(errorFormat, transactionId, "Amount should be decimal number"));

            if (!CurrencyCodeHelper.IsCurrencyFormatCorrect(transaction.PaymentDetails.CurrencyCode))
                errorList.Add(string.Format(errorFormat, transactionId, "Currency Code is not correct"));

            if (!DateTime.TryParseExact(transaction.TransactionDate, "yyyy-MM-ddTHH:mm:ss", null, DateTimeStyles.None, out _))
                errorList.Add(string.Format(errorFormat, transactionId, "Transaction Date format is not correct"));

            if (!transactionStatus.Contains(transaction.Status))
                errorList.Add(string.Format(errorFormat, transactionId, "Transaction Status is not correct"));

            return errorList;
        }
    }
}
