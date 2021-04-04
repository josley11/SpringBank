using System.Globalization;
using System.Text.RegularExpressions;

namespace SpringBank.Common
{
    public static class Validator
    {
        /// <summary>
        /// Validates whether the givenn amount is a valid currency amount
        /// </summary>
        public static bool IsValidCurrencyAmount(decimal amount)
        {
            var value = amount.ToString(CultureInfo.InvariantCulture);
            return Regex.IsMatch(value, @"^\d+(\.\d{1,2})?$");
        }
    }
}
