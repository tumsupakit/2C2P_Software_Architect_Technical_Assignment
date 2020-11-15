using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.Interfaces;

namespace Transaction.BusinessLogic
{
    public class FileValidator : IFileValidator
    {
        public string Validate(IFormFile file)
        {
            string errorMessage = string.Empty;
            long MAX_SIZE = 1048576;

            if (!file.FileName.EndsWith(".csv") && !file.FileName.EndsWith(".xml"))
                errorMessage = "Type of the file is not support";
            if (file.Length > MAX_SIZE)
                errorMessage = "The file larger than 1 MB";

            return errorMessage;
        }
    }
}
