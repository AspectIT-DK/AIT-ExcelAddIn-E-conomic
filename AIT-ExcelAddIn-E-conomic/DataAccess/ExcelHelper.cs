using Excel = Microsoft.Office.Interop.Excel;

namespace AIT_ExcelAddIn_E_conomic.DataAccess
{
    public static class ExcelHelper
    {
        public static Excel.Range GetSelectedRows()
        {
            var app = Globals.ThisAddIn.Application as Excel.Application;
            Excel.Range Range = app.Selection as Excel.Range;
            Range = Range.EntireRow;

            return Range;
        }

    }
}
