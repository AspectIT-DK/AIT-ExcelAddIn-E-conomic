using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class VatZoneCollection
    {
        [JsonPropertyName("collection")]
        public List<VatZone> Collection { get; set; }
    }
}
