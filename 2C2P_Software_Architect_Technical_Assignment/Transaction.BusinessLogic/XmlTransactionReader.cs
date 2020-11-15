using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Transaction.BusinessLogic.Interfaces;
using Transaction.BusinessLogic.ViewModels;
using Transaction.Domain;

namespace Transaction.BusinessLogic
{
    public class XmlTransactionReader : IXmlTransactionReader
    {
        public XmlTransactionModel Read(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var serializer = new XmlSerializer(typeof(XmlTransactionModel));
            var xmlTransactionModel = (XmlTransactionModel)serializer.Deserialize(XmlReader.Create(stream));

            return xmlTransactionModel;
        }
    }
}
