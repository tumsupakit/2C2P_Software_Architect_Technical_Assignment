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
        ICsvValidator csvValidator;


        public TransactionService(ITransactionRepository transactionRepository, 
            IFileValidator fileValidator, 
            ITransactionReader transactionReader, 
            IXmlValidator xmlValidator,
            ICsvValidator csvValidator)
        {
            this.transactionRepository = transactionRepository;
            this.fileValidator = fileValidator;
            this.transactionReader = transactionReader;
            this.xmlValidator = xmlValidator;
            this.csvValidator = csvValidator;
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
                        CallCsvReader(file, ref errorMessages);
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

        private void CallCsvReader(IFormFile file, ref List<string> errorMesages)
        {
            var Reader = transactionReader as CsvTransactionReader;

            var csvTransactionModels = (List<CsvTransactionModel>)Reader.Read(file);
            if (csvTransactionModels.Count > 0)
            {
                foreach (CsvTransactionModel transaction in csvTransactionModels)
                {
                    errorMesages.AddRange(csvValidator.Validate(transaction));
                }

                if (errorMesages.Count == 0)
                {
                    IEnumerable<TransactionModel> transactionModels = csvTransactionModels.Select(s => s.MapToModel());
                    transactionRepository.AddRange(transactionModels);
                }
            }
        }

        private void CallXmlReader(IFormFile file, ref List<string> errorMesages)
        {
            var Reader = transactionReader as XmlTransactionReader;

            XmlTransactionModel xmlTransactionModel = (XmlTransactionModel)Reader.Read(file);
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
