using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Domain;

namespace Transaction.DataAccess.Mapping
{
    public static class TransactionMap
    {
        public static ModelBuilder MapTransaction(this ModelBuilder modelBuilder) 
        {
            var entity = modelBuilder.Entity<TransactionModel>();
            entity.ToTable("Transactions");
            entity.HasKey(k => k.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();

            return modelBuilder;
        }
    }
}
