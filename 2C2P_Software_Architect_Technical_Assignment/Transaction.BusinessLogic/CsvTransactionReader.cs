using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Transaction.BusinessLogic.Interfaces;
using Transaction.BusinessLogic.ViewModels;

namespace Transaction.BusinessLogic
{
    public class CsvTransactionReader : ICsvTransactionReader
    {
        public List<CsvTransactionModel> Read(IFormFile file)
        {
            List<string> listData = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream())) 
            {
                while (reader.Peek() >= 0) 
                {
                    string data = reader.ReadLine();
                    data = string.Join(",", data.Split(',').Select(s => s.Trim()).ToArray());
                    if (data.Split("\",\"").Length != 5)
                        throw new InvalidOperationException();
                    else
                        listData.Add(data);
                }   
            }

            return ConvertListDataToModel(listData);
        }

        private List<CsvTransactionModel> ConvertListDataToModel(List<string> listData) 
        {
            var csvTransactionModels = new List<CsvTransactionModel>();

            foreach (string data in listData)
            {
                string[] items = data.Split("\",\"");
                csvTransactionModels.Add(new CsvTransactionModel
                {
                    TransactionId = items[0].Replace("\"",""),
                    Amount = items[1],
                    CurrencyCode = items[2],
                    TransactionDate = items[3],
                    Status = items[4].Replace("\"", "")
                });
            }

            return csvTransactionModels;
        }
    }
}
