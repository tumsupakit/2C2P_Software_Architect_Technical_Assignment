using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.ViewModels;

namespace Transaction.BusinessLogic.Interfaces
{
    public interface IXmlTransactionReader
    {
        XmlTransactionModel Read(IFormFile file);
    }
}
