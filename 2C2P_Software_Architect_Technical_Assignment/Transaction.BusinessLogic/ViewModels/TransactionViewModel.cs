﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.BusinessLogic.ViewModels
{
    public class TransactionViewModel
    {
        public string Id { get; set; }
        public string CurrencyCode { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
    }
}
