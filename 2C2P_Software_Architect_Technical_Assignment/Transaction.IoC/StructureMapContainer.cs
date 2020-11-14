using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Transaction.Persistence;

namespace Transaction.IoC
{
    public static class StructureMapContainer
    {
        public static IServiceCollection RegisterIoC(this IServiceCollection services) 
        {
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}
