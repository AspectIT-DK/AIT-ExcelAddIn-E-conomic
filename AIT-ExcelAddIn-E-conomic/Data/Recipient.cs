using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Recipient
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("vatZone")]
        public VatZone VatZone { get; set; }
    }
}
