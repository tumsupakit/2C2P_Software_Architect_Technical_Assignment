using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using Transaction.BusinessLogic;
using Transaction.BusinessLogic.Interfaces;

namespace Transaction.Test
{
    [TestClass]
    public class FileValidatorTest
    {
        IFileValidator fileValidator;

        public FileValidatorTest()
        {
            fileValidator = new FileValidator();
        }

        [TestMethod]
        public void Validate_Xml_File_Type_Pass()
        {
            var fileMock = new Mock<IFormFile>();

            string fileName = "test.xml";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            IFormFile file = fileMock.Object;

            string result = fileValidator.Validate(file);
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Validate_Csv_File_Type_Pass()
        {
            var fileMock = new Mock<IFormFile>();

            string fileName = "test.csv";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            IFormFile file = fileMock.Object;

            string result = fileValidator.Validate(file);
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Validate_Excel_File_Format_xlsx_Fail()
        {
            var fileMock = new Mock<IFormFile>();

            string fileName = "test.xlsx";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            IFormFile file = fileMock.Object;

            string result = fileValidator.Validate(file);
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Validate_Pdf_File__Fail()
        {
            var fileMock = new Mock<IFormFile>();

            string fileName = "test.pdf";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            IFormFile file = fileMock.Object;

            string result = fileValidator.Validate(file);
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Validate_PNG_File_Format_Fail()
        {
            var fileMock = new Mock<IFormFile>();

            string fileName = "test.png";
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            
            IFormFile file = fileMock.Object;

            string result = fileValidator.Validate(file);
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Validate_File_Size_Format_Pass()
        {
            var fileMock = new Mock<IFormFile>();

            string fileName = "test.csv";

            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(1000);

            IFormFile file = fileMock.Object;

            string result = fileValidator.Validate(file);
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Validate_File_Size_Format_Fail()
        {
            var fileMock = new Mock<IFormFile>();

            string fileName = "test.csv";

            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(100000000);

            IFormFile file = fileMock.Object;

            string result = fileValidator.Validate(file);
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }
}
