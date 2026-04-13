using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class ProductCollection
    {
        [JsonPropertyName("collection")]
        public List<Product> Collection { get; set; }
    }
}
