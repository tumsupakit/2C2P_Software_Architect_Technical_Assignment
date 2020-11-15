using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.Helpers;
using Transaction.BusinessLogic.Interfaces;

namespace Transaction.BusinessLogic
{
    public class CurrencyService : ICurrencyService
    {
        public string[] Get()
        {
            return CurrencyCodeHelper.GetAllCurrency();
        }
    }
}
