using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    /*
    * ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    *  This is a JSON Helper object
    * ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    */
    public class CollectionOf<T>
    {
        [JsonPropertyName("collection")]
        public List<T> Collection { get; set; }
    }
}
