using AIT_ExcelAddIn_E_conomic.DataAccess;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Customer
    {
        [JsonPropertyName("customerNumber")]
        public int CustomerNumber { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("zip")]
        public string Postcode { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("ean")]
        public string EAN { get; set; }
        [JsonPropertyName("corporateIdentificationNumber")]
        public string CompanyNumber { get; set; }
        [JsonPropertyName("paymentTerms")]
        public PaymentTerms PaymentTerms { get; set; }
        [JsonPropertyName("vatZone")]
        public VatZone VatZone { get; set; }
        [JsonPropertyName("layout")]
        public Layout Layout { get; set; }
        [JsonPropertyName("customerGroup")]
        public CustomerGroup CustomerGroup { get; set; }
        //[JsonPropertyName("attention")]
        //public string Attention { get; set; }

        public Customer() {}
    }
}
