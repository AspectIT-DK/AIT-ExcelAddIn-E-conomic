using AIT_ExcelAddIn_E_conomic.Configuration;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using System;
using System.Globalization;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

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
                ShowError($"Unable to parse: '{Input}' as decimal number");
                return false;
            }
            return true;
        }
        public static bool ParseInt(string Input, out int Result)
        {
            if (!int.TryParse(Input, NumberStyles.Integer, NumberFormatInfo, out Result))
            {
                ShowError($"Unable to parse: '{Input}' as whole number");
                return false;
            }
            return true;
        }
        public static void ShowError(string Message, string Title = "Error", MessageBoxButton Button = MessageBoxButton.OK, MessageBoxImage Image = MessageBoxImage.Error)
        {
            if(!ShowErrors) { return; }
            MessageBox.Show(Message, Title, Button, Image);
        }
        public static void MarkRow(Excel.Range SingleRow, RowErrorState RowErrorState)
        {
            System.Drawing.Color Color;
            switch (RowErrorState)
            {
                case RowErrorState.None:
                    Color = System.Drawing.Color.White;
                    break;
                case RowErrorState.Ok:
                    Color = Settings.RowColorSettings.RowColorSuccessOperation;
                    break;
                case RowErrorState.Bad:
                    Color = Settings.RowColorSettings.RowColorFailOperation;
                    break;
                case RowErrorState.InvoiceSuccess:
                    Color = Settings.RowColorSettings.RowColorInvoiceSuccess;
                    break;
                case RowErrorState.InvoiceFail:
                    Color = Settings.RowColorSettings.RowColorInvoiceFail;
                    break;
                default:
                    Color = System.Drawing.Color.White;
                    break;
            }
            ExcelHelper.SetColorOfRow(SingleRow, Color);
        }
        public static void MarkRowInvoiceSuccess(Excel.Range SingleRow)
        {
            MarkRow(SingleRow, RowErrorState.InvoiceSuccess);
        }
        public static void MarkRowInvoiceFail(Excel.Range SingleRow)
        {
            MarkRow(SingleRow, RowErrorState.InvoiceFail);
        }
        public static void MarkRowInvoiceFail(int RowNumber)
        {
            Excel.Range SingleRow = ExcelHelper.RowNumberToExcelRow(RowNumber);
            MarkRow(SingleRow, RowErrorState.InvoiceFail);
        }
        public static void MarkRowInvoiceSuccess(int RowNumber)
        {
            Excel.Range SingleRow = ExcelHelper.RowNumberToExcelRow(RowNumber);
            MarkRow(SingleRow, RowErrorState.InvoiceSuccess);
        }
        [Flags]
        public enum RowErrorState
        {
            None = 0,
            Ok = 1,
            Bad = 2,
            InvoiceSuccess = 4,
            InvoiceFail = 8
        }
    }
}
