using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.BusinessLogic.Interfaces
{
    public interface IFileValidator
    {
        string Validate(IFormFile file);
    }
}
