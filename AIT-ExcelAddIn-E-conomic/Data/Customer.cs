using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Customer
    {
        [JsonPropertyName("customerNumber")]
        public int CustomerNumber { get; set; }
    }
}
