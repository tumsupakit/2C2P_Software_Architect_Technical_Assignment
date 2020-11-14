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
        public static TransactionViewModel MapToViewModel(this TransactionModel model) 
        {
            TransactionViewModel viewModel = null;

            if (model != null) 
            {
                viewModel = new TransactionViewModel()
                {
                    Id = model.TransactionId,
                    Payment = $"{model.Amount.ToString(".00") } {model.CurrencyCode}"
                };

                if (model.Status == TransactionStatus.Approved)
                    viewModel.Status = "A";
                else if (model.Status == TransactionStatus.Rejected)
                    viewModel.Status = "R";
                else
                    viewModel.Status = "D";
            }

            return viewModel;
        }
    }
}
