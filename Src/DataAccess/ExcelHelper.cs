using AIT_ExcelAddIn_E_conomic.Configuration;
using Excel = Microsoft.Office.Interop.Excel;

namespace AIT_ExcelAddIn_E_conomic.DataAccess
{
    public static class ExcelHelper
    {
        private static Excel.Application App = Globals.ThisAddIn.Application as Excel.Application;
        public static Excel.Range GetSelectedRows()
        {
            Excel.Range Range = App.Selection as Excel.Range;

            return Range.EntireRow;
        }
        public static Excel.Range GetSelectedCells()
        {
            Excel.Range Range = App.Selection as Excel.Range;

            return Range;
        }
        public static void SetColorOfRow(Excel.Range SingleRow, System.Drawing.Color Color)
        {
            if (!Settings.RowColorSettings.AllowRowsToBeColored) { return; } // User preference doesn't allow to change row color; Commit die.
            if (SingleRow == null) { return; } // No rows to color; Commit die.
            Excel.Range UsedRange = ((Excel.Worksheet)App.ActiveSheet).UsedRange;
            Excel.Range TargetRow = App.Intersect(UsedRange, SingleRow);
            
            TargetRow.Interior.Color = Color;
        }
        public static Excel.Range RowNumberToExcelRow(int RowNumber)
        {
            Excel.Worksheet ActiveSheet = App.ActiveSheet as Excel.Worksheet;
            Excel.Range Row = ActiveSheet.Cells[RowNumber, 1] as Excel.Range;
            return Row.EntireRow;
        }
    }
}
