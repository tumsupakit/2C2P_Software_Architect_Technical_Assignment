using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic;
using Transaction.BusinessLogic.Interfaces;
using Transaction.Persistence;

namespace Transaction.IoC
{
    public static class StructureMapContainer
    {
        public static IServiceCollection RegisterIoC(this IServiceCollection services) 
        {
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IFileValidator, FileValidator>();
            services.AddTransient<IXmlTransactionReader, XmlTransactionReader>();
            services.AddTransient<ICsvTransactionReader, CsvTransactionReader>();
            services.AddTransient<IXmlValidator, XmlValidator>();
            services.AddTransient<ICsvValidator, CsvValidator>();

            return services;
        }
    }
}
