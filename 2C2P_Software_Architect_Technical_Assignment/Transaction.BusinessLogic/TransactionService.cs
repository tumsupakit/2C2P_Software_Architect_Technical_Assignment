using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Transaction.BusinessLogic.Interfaces;
using Transaction.BusinessLogic.ModelMappers;
using Transaction.BusinessLogic.ViewModels;
using Transaction.Domain;
using Transaction.Domain.Enum;
using Transaction.Persistence;

namespace Transaction.BusinessLogic
{
    public class TransactionService : ITransactionService
    {
        ITransactionRepository transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public IEnumerable<TransactionViewModel> Filter(TransactionFilter filter)
        {
            IEnumerable<TransactionModel> transactions = transactionRepository.Filter(filter.currencyCode, filter.dateFrom, filter.dateTo, filter.status);

            return transactions.Select(s => s.MapToViewModel());
        }

        public IList<TransactionModel> ReadFile(Stream stream, string format)
        {
            throw new NotImplementedException();
        }
    }
}
