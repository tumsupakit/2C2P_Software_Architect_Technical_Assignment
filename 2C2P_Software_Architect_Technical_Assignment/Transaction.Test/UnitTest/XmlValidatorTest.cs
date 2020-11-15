using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic;
using Transaction.BusinessLogic.Interfaces;
using Transaction.BusinessLogic.ViewModels;

namespace Transaction.Test.UnitTest
{
    [TestClass]
    public class XmlValidatorTest
    {
        IXmlValidator xmlValidator;

        public XmlValidatorTest()
        {
            this.xmlValidator = new XmlValidator();
        }

        [TestMethod]
        public void Validate_PerfectFile_Pass() 
        {
            var xmlTransaction = new XmlTransaction
            {
                id = "Inv00001",
                TransactionDate = "2019-01-23T13:45:10",
                PaymentDetails = new PaymentDetails 
                { 
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                Status = "Done"
            };

            List<string> result = xmlValidator.Validate(xmlTransaction);

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Validate_IDLengthTooLong_Fail()
        {
            var xmlTransaction = new XmlTransaction
            {
                id = "Inv0000128352374283751028337345734592485343945347537420348457320384273",
                TransactionDate = "2019-01-23T13:45:10",
                PaymentDetails = new PaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                Status = "Done"
            };

            List<string> result = xmlValidator.Validate(xmlTransaction);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].EndsWith("Transaction ID length should be less than 50"));
        }

        [TestMethod]
        public void Validate_WrongTransactionDateFormat_Fail()
        {
            var xmlTransaction = new XmlTransaction
            {
                id = "Inv00001",
                TransactionDate = "2019/01/23 13:45:10",
                PaymentDetails = new PaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                Status = "Done"
            };

            List<string> result = xmlValidator.Validate(xmlTransaction);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].EndsWith("Transaction Date format is not correct"));
        }

        [TestMethod]
        public void Validate_AmountValueIsNotANumber_Fail()
        {
            var xmlTransaction = new XmlTransaction
            {
                id = "Inv00001",
                TransactionDate = "2019-01-23T13:45:10",
                PaymentDetails = new PaymentDetails
                {
                    Amount = "AAA",
                    CurrencyCode = "USD"
                },
                Status = "Done"
            };

            List<string> result = xmlValidator.Validate(xmlTransaction);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].EndsWith("Amount should be decimal number"));
        }

        [TestMethod]
        public void Validate_CurrencyCodeIsNotCorrent_Fail()
        {
            var xmlTransaction = new XmlTransaction
            {
                id = "Inv00001",
                TransactionDate = "2019-01-23T13:45:10",
                PaymentDetails = new PaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "AAA"
                },
                Status = "Done"
            };

            List<string> result = xmlValidator.Validate(xmlTransaction);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].EndsWith("Currency Code is not correct"));
        }

        [TestMethod]
        public void Validate_StatusCodeIsNotCorrent_Fail()
        {
            var xmlTransaction = new XmlTransaction
            {
                id = "Inv00001",
                TransactionDate = "2019-01-23T13:45:10",
                PaymentDetails = new PaymentDetails
                {
                    Amount = "200.00",
                    CurrencyCode = "USD"
                },
                Status = "AAA"
            };

            List<string> result = xmlValidator.Validate(xmlTransaction);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0].EndsWith("Transaction Status is not correct"));
        }

        [TestMethod]
        public void Validate_MultipleErrorsOccur_Fail()
        {
            var xmlTransaction = new XmlTransaction
            {
                id = "Inv00001",
                TransactionDate = "2019-01-23T13:45:10",
                PaymentDetails = new PaymentDetails
                {
                    Amount = "AAA",
                    CurrencyCode = "AAA"
                },
                Status = "AAA"
            };

            List<string> result = xmlValidator.Validate(xmlTransaction);

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result[0].EndsWith("Amount should be decimal number"));
            Assert.IsTrue(result[1].EndsWith("Currency Code is not correct"));
            Assert.IsTrue(result[2].EndsWith("Transaction Status is not correct"));
        }
    }
}
