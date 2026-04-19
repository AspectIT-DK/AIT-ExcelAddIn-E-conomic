using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class LayoutCollection
    {
        [JsonPropertyName("collection")]
        public List<Layout> Collection { get; set; }
    }
}
