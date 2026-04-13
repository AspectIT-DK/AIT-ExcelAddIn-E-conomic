using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class InvoiceLine
    {
        [JsonPropertyName("lineNumber")]
        public int LineNumber { get; set; }
        [JsonPropertyName("sortKey")]
        public int SortKey { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("unit")]
        public Unit Unit { get; set; }
        [JsonPropertyName("product")]
        public Product Product { get; set; }
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        [JsonPropertyName("unitNetPrice")]
        public decimal UnitNetPrice { get; set; }
        public static InvoiceLine GetLineSeparator(int LineNumber = 99, int SortKey = 99)
        {
            return new InvoiceLine { LineNumber = LineNumber, SortKey = SortKey };
        }
    }
}
