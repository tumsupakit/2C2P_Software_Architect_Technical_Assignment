using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.ViewModels;
using Transaction.Domain;

namespace Transaction.BusinessLogic.Interfaces
{
    public interface ITransactionReader
    {
        XmlTransactionModel Read(IFormFile file);
    }
}
