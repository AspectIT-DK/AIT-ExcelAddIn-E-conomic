using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Product
    {
        [JsonPropertyName("productNumber")]
        public string ProductNumber { get; set; } // Intentionally a string. 
    }
}
