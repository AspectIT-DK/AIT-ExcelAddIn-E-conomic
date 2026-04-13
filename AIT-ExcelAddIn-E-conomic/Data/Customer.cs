using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Customer
    {
        [JsonPropertyName("customerNumber")]
        public int CustomerNumber { get; set; }
        [JsonPropertyName("paymentTerms")]
        public PaymentTerms PaymentTerms { get; set; }
        [JsonPropertyName("vatZone")]
        public VatZone VatZone { get; set; }
        [JsonPropertyName("layout")]
        public Layout Layout { get; set; }
        [JsonPropertyName("customerGroup")]
        public CustomerGroup CustomerGroup { get; set; }
    }
}
