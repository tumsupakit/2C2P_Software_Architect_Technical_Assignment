using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Transaction.BusinessLogic.ViewModels;
using Transaction.Domain;
using Transaction.Domain.Enum;

namespace Transaction.BusinessLogic.Interfaces
{
    public interface ITransactionService 
    {
        IEnumerable<TransactionViewModel> Filter(TransactionFilter filter);
        IList<TransactionModel> ReadFile(Stream stream, string format);
    }
}
