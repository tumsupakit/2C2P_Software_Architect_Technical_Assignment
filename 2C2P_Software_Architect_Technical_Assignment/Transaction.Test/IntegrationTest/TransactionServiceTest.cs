using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Transaction.BusinessLogic;
using Transaction.BusinessLogic.Interfaces;
using Transaction.Domain;
using Transaction.Persistence;

namespace Transaction.Test.IntegrationTest
{
    [TestClass]
    public class TransactionServiceTest
    {
        private Mock<ITransactionRepository> mockTransactionRepository;

        public TransactionServiceTest()
        {
            mockTransactionRepository = new Mock<ITransactionRepository>();
        }

        [TestMethod]
        public void Upload_PdfIntactFile_Pass() 
        {
            ITransactionService transactionService = new TransactionService(
                mockTransactionRepository.Object, new FileValidator(), new XmlTransactionReader(), new XmlValidator());

            var fileMock = new Mock<IFormFile>();
            string fileName = "test.xml";
            string content = 
                "<Transactions> " +
                "<Transaction id=\"Inv00001\"> " +
                "<TransactionDate>2019-01-23T13:45:10</TransactionDate> " +
                "<PaymentDetails> <Amount>200.00</Amount> <CurrencyCode>USD</CurrencyCode> </PaymentDetails> " +
                "<Status>Done</Status> </Transaction> " +
                "</Transactions>";

            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            IFormFile file = fileMock.Object;
            IList<string> errorMessages = transactionService.Upload(file);

            Assert.IsTrue(errorMessages.Count == 0);
            mockTransactionRepository.Verify(v => v.AddRange(It.IsAny<IEnumerable<TransactionModel>>()), Times.Once);
        }

        [TestMethod]
        public void Upload_CorruptPdfFile_Fail()
        {
            ITransactionService transactionService = new TransactionService(
                mockTransactionRepository.Object, new FileValidator(), new XmlTransactionReader(), new XmlValidator());

            var fileMock = new Mock<IFormFile>();
            string fileName = "test.xml";
            string content =
                "<Transactions> " +
                "<Transaction id=\"Inv00001\"> " +
                "<TransactionDate></TransactionDate> " +
                "<PaymentDetails> <Amount>200.00</Amount> <CurrencyCode>AAAA</CurrencyCode> </PaymentDetails> " +
                "<Status>Done</Status> </Transaction> " +
                "</Transactions>";

            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            IFormFile file = fileMock.Object;
            IList<string> errorMessages = transactionService.Upload(file);

            Assert.IsTrue(errorMessages.Count > 0);
            mockTransactionRepository.Verify(v => v.AddRange(It.IsAny<IEnumerable<TransactionModel>>()), Times.Never);
        }

    }
}
