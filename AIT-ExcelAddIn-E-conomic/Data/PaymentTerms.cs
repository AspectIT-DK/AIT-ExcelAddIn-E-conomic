using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class PaymentTerms
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null;
        [JsonPropertyName("paymentTermsNumber")]
        public int PaymentTermsNumber { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }


        public override string ToString()
        {
            return ($"PaymentTerms - Name: {Name}, Number: {PaymentTermsNumber}, Desc: {Description}");
        }
    }
}
