using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Unit
    {
        [JsonPropertyName("unitNumber")]
        public int UnitNumber { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
