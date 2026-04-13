using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class UnitCollection
    {
        [JsonPropertyName("collection")]
        public List<Unit> Collection { get; set; }
    }
}
