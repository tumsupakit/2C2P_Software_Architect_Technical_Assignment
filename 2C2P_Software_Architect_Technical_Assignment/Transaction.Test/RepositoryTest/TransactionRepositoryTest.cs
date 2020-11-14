using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transaction.DataAccess;
using Transaction.Domain;
using Transaction.Domain.Enum;
using Transaction.Persistence;

namespace Transaction.Test.RepositoryTest
{
    [TestClass]
    public class TransactionRepositoryTest
    {
        
        [TestMethod]
        public void Get_Transactions_With_No_Filter_FOUND_ALL()
        {
            Mock<ITransactionRepository> mockRepository = new Mock<ITransactionRepository>();

            List<TransactionModel> mockData = new List<TransactionModel>()
            {
                new TransactionModel{ Id = 1, TransactionId = "1111", TransactionDate = DateTime.Now, Amount = 10, CurrencyCode = "USD", Status = TransactionStatus.Approved },
                new TransactionModel{ Id = 2, TransactionId = "2222", TransactionDate = DateTime.Now, Amount = 20, CurrencyCode = "THB", Status = TransactionStatus.Finished }
            };

            string currencyCode = string.Empty;
            DateTime? dateFrom = null, dateTo = null;
            TransactionStatus? transactionStatus = null;

            mockRepository.Setup(m => m.Filter(currencyCode, dateFrom, dateTo, transactionStatus)).Returns(mockData);

            List<TransactionModel> results = mockRepository.Object.Filter(currencyCode, dateFrom, dateTo, transactionStatus).ToList();

            mockRepository.Verify(v => v.Filter(currencyCode, dateFrom, dateTo, transactionStatus), Times.Once);

            Assert.AreEqual(mockData.Count, results.Count);
        }

        [TestMethod]
        public void Get_Transactions_With_USD_ConcurrencyCode_Filter_FOUN_ONE()
        {
            Mock<ITransactionRepository> mockRepository = new Mock<ITransactionRepository>();

            TransactionModel usdData = new TransactionModel { Id = 1, TransactionId = "1111", TransactionDate = DateTime.Now, Amount = 10, CurrencyCode = "USD", Status = TransactionStatus.Approved };
            TransactionModel thbData = new TransactionModel { Id = 2, TransactionId = "2222", TransactionDate = DateTime.Now, Amount = 20, CurrencyCode = "THB", Status = TransactionStatus.Finished };

            List<TransactionModel> mockData = new List<TransactionModel>() { usdData, thbData };
            List<TransactionModel> mockUsd = new List<TransactionModel> { usdData };

            string emptyCurrencyCode = string.Empty;
            string usdCurrencyCode = "USD";
            DateTime? dateFrom = null, dateTo = null;
            TransactionStatus? transactionStatus = null;

            mockRepository.Setup(m => m.Filter(emptyCurrencyCode, dateFrom, dateTo, transactionStatus)).Returns(mockData);
            mockRepository.Setup(m => m.Filter(usdCurrencyCode, dateFrom, dateTo, transactionStatus)).Returns(mockUsd);

            List<TransactionModel> results = mockRepository.Object.Filter(usdCurrencyCode, dateFrom, dateTo, transactionStatus).ToList();

            mockRepository.Verify(v => v.Filter(emptyCurrencyCode, dateFrom, dateTo, transactionStatus), Times.Never);
            mockRepository.Verify(v => v.Filter(usdCurrencyCode, dateFrom, dateTo, transactionStatus), Times.Once);

            Assert.AreEqual(usdData.CurrencyCode, results[0].CurrencyCode);
        }

        [TestMethod]
        public void Get_Transactions_With_JPY_Filter_NOT_FOUND()
        {
            Mock<ITransactionRepository> mockRepository = new Mock<ITransactionRepository>();

            TransactionModel usdData = new TransactionModel { Id = 1, TransactionId = "1111", TransactionDate = DateTime.Now, Amount = 10, CurrencyCode = "USD", Status = TransactionStatus.Approved };
            TransactionModel thbData = new TransactionModel { Id = 2, TransactionId = "2222", TransactionDate = DateTime.Now, Amount = 20, CurrencyCode = "THB", Status = TransactionStatus.Finished };

            List<TransactionModel> mockData = new List<TransactionModel>() { usdData, thbData };
            List<TransactionModel> mockUsd = new List<TransactionModel> { usdData };

            string emptyCurrencyCode = string.Empty;
            string jpyCurrencyCode = "JPY";
            DateTime? dateFrom = null, dateTo = null;
            TransactionStatus? transactionStatus = null;

            mockRepository.Setup(m => m.Filter(emptyCurrencyCode, dateFrom, dateTo, transactionStatus)).Returns(mockData);

            List<TransactionModel> results = mockRepository.Object.Filter(jpyCurrencyCode, dateFrom, dateTo, transactionStatus).ToList();

            mockRepository.Verify(v => v.Filter(emptyCurrencyCode, dateFrom, dateTo, transactionStatus), Times.Never);
            mockRepository.Verify(v => v.Filter(jpyCurrencyCode, dateFrom, dateTo, transactionStatus), Times.Once);

            Assert.AreEqual(0, results.Count);
        }
    }
}
