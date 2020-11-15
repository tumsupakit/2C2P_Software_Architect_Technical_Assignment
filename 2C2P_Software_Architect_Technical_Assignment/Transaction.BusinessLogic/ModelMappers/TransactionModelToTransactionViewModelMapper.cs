using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.ViewModels;
using Transaction.Domain;
using Transaction.Domain.Enum;

namespace Transaction.BusinessLogic.ModelMappers
{
    public static class TransactionModelToTransactionViewModelMapper
    {
        public static TransactionViewModel MapToViewModel(this TransactionModel item) 
        {
            TransactionViewModel viewModel = null;

            if (item != null) 
            {
                viewModel = new TransactionViewModel()
                {
                    Id = item.TransactionId,
                    Payment = item.Amount.ToString(".00") + " " + item.CurrencyCode
                };

                if (item.Status == TransactionStatus.Approved)
                    viewModel.Status = "A";
                else if (item.Status == TransactionStatus.Rejected)
                    viewModel.Status = "R";
                else
                    viewModel.Status = "D";
            }

            return viewModel;
        }
    }
}
