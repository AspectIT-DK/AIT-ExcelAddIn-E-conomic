using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class CustomerGroup
    {
        [JsonPropertyName("customerGroupNumber")]
        public int CustomerGroupNumber { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("layout")]
        public Layout Layout { get; set; }
    }
}
