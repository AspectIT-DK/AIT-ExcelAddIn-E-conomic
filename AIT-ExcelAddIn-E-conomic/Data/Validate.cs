using AIT_ExcelAddIn_E_conomic.Configuration;
using System.Globalization;
using System.Windows;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public static class Validate
    {
        public static bool ShowErrors = true;
        public static NumberFormatInfo NumberFormatInfo = new NumberFormatInfo();
        static Validate()
        {
            NumberFormatInfo.CurrencyDecimalSeparator = Settings.FieldMap["CultureDecimalDelimiter"];
        }
        public static bool ParseDecimal(string Input, out decimal Result)
        {
            if(!decimal.TryParse(Input, NumberStyles.Float | NumberStyles.AllowDecimalPoint, NumberFormatInfo, out Result))
            {
                if(ShowErrors)
                {
                    MessageBox.Show($"Unable to parse: '{Input}' as decimal number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return false;
            }
            // DEBUG: MessageBox.Show($"Parsed: '{Input}' as decimal number. Whole number part: {Result}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }
        public static bool ParseInt(string Input, out int Result)
        {
            if (!int.TryParse(Input, NumberStyles.Integer, NumberFormatInfo, out Result))
            {
                if (ShowErrors)
                {
                    MessageBox.Show($"Unable to parse: '{Input}' as whole number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return false;
            }
            return true;
        }
    }
}
