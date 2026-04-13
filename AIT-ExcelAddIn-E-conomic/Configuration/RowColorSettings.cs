using System.Drawing;

namespace AIT_ExcelAddIn_E_conomic.Configuration
{
    public class RowColorSettings
    {
        public Color RowColorSuccessOperation { get; set; }
        public Color RowColorFailOperation { get; set; }
        public Color RowColorInvoiceSuccess { get; set; }
        public Color RowColorInvoiceFail { get; set; }
        public bool AllowRowsToBeColored { get; set; }
    }
}
