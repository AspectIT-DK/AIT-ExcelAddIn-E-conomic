using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Layout
    {
        [JsonPropertyName("layoutNumber")]
        public int    LayoutNumber { get; set; }
        [JsonPropertyName("name")]
        public string Name        { get; set; } = null;
        [JsonPropertyName("deleted")]
        public bool?  Deleted     { get; set; } = null;
        [JsonPropertyName("self")]
        public string Self        { get; set; } = null;

        public override string ToString()
        {
            return ($"#{LayoutNumber} {Name}");
        }
    }
}
