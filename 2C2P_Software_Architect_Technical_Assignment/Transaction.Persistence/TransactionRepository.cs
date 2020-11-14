using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transaction.DataAccess;
using Transaction.Domain;
using Transaction.Domain.Enum;

namespace Transaction.Persistence
{
    public class TransactionRepository : ITransactionRepository
    {
        private DataContext dataContext;

        public TransactionRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void AddRange(IEnumerable<TransactionModel> transactions)
        {
            this.dataContext.Transaction.AddRange(transactions);
            this.dataContext.SaveChanges();
        }

        public IEnumerable<TransactionModel> Filter(string currencyCode, DateTime? dateFrom, DateTime? dateTo, TransactionStatus? status)
        {
            DateTime dateFromFilter = dateFrom ?? DateTime.MinValue;
            DateTime dateToFilter = dateTo ?? DateTime.MaxValue;

            IQueryable<TransactionModel> query = this.dataContext.Transaction.Where(w => w.TransactionDate >= dateFromFilter && w.TransactionDate <= dateToFilter);

            if (!string.IsNullOrEmpty(currencyCode))
                query = query.Where(w => w.CurrencyCode == currencyCode);

            if(status.HasValue)
                query = query.Where(w => w.Status == status.Value);

            return query.AsEnumerable();
        }
    }
}
