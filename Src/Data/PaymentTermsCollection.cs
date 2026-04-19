using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class PaymentTermsCollection
    {
        [JsonPropertyName("collection")]
        public List<PaymentTerms> Collection { get; set; }
    }
}
