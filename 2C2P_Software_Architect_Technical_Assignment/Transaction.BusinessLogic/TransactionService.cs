using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
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
        IFileValidator fileValidator;
        ITransactionReader transactionReader;
        IXmlValidator xmlValidator;


        public TransactionService(ITransactionRepository transactionRepository, 
            IFileValidator fileValidator, 
            ITransactionReader transactionReader, 
            IXmlValidator xmlValidator)
        {
            this.transactionRepository = transactionRepository;
            this.fileValidator = fileValidator;
            this.transactionReader = transactionReader;
            this.xmlValidator = xmlValidator;
        }

        public IEnumerable<TransactionViewModel> Filter(TransactionFilter filter)
        {
            IEnumerable<TransactionModel> transactions = transactionRepository.Filter(filter.currencyCode, filter.dateFrom, filter.dateTo, filter.status);

            return transactions.Select(s => s.MapToViewModel());
        }

        public IList<string> Upload(IFormFile file)
        {
            var errorMessages = new List<string>();

            string error = fileValidator.Validate(file);

            if (!string.IsNullOrEmpty(error))
                errorMessages.Add(error);
            else 
            {
                try
                {
                    if (file.FileName.Contains(".csv"))
                    {

                    }
                    else
                        CallXmlReader(file, ref errorMessages);
                }
                catch (InvalidOperationException)
                {
                    errorMessages.Add("Unknown format");
                }
                catch (Exception ex)
                {
                    errorMessages.Add(ex.Message);
                }
            }   

            return errorMessages;
        }

        private void CallXmlReader(IFormFile file, ref List<string> errorMesages)
        {
            var Reader = transactionReader as XmlTransactionReader;

            XmlTransactionModel xmlTransactionModel = Reader.Read(file);
            if (xmlTransactionModel != null) 
            {
                foreach (XmlTransaction transaction in xmlTransactionModel.Transactions)
                {
                    errorMesages.AddRange(xmlValidator.Validate(transaction));
                }

                if (errorMesages.Count == 0) 
                {
                    IEnumerable<TransactionModel> transactionModels = xmlTransactionModel.MapToModel();
                    transactionRepository.AddRange(transactionModels);
                }
            }
        }
    }
}
