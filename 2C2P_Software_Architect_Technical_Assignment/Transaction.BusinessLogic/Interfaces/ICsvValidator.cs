using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.ViewModels;

namespace Transaction.BusinessLogic.Interfaces
{
    public interface ICsvValidator
    {
        List<string> Validate(CsvTransactionModel transaction);
    }
}
