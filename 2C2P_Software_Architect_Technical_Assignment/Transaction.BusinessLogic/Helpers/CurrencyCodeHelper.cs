using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Transaction.BusinessLogic.Helpers
{
    public static class CurrencyCodeHelper
    {
        public static bool IsCurrencyFormatCorrect(string currencyCode) 
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            .Select(x => (new RegionInfo(x.LCID)).ISOCurrencySymbol)
            .Contains(currencyCode);
        }
    }
}
