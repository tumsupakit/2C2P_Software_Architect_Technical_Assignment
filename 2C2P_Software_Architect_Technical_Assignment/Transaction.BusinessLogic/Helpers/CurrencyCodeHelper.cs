using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Transaction.BusinessLogic.Helpers
{
    public static class CurrencyCodeHelper
    {
        public static bool IsCurrencyFormatCorrect(string currencyCode) => GetAllCurrency().Contains(currencyCode);

        public static string[] GetAllCurrency() 
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                    .Select(s => (new RegionInfo(s.LCID)).ISOCurrencySymbol)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToArray();
        }
            
    }
}
