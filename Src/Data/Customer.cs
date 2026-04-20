using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    /*
	* ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	*  Documentation: https://restapi.e-conomic.com/schema/customers.customerNumber.get.schema.json
	* ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	*/
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
        public string CompanyNumber { get; set; } // CVR
        [JsonPropertyName("publicEntryNumber")]
        public string PublicEntryNumber { get; set; }
        [JsonPropertyName("paymentTerms")]
        public PaymentTerms PaymentTerms { get; set; }
        [JsonPropertyName("vatZone")]
        public VatZone VatZone { get; set; }
        [JsonPropertyName("vatNumber")]
        public string VatNumber { get; set; } // Not CVR
        [JsonPropertyName("layout")]
        public Layout Layout { get; set; }
        [JsonPropertyName("customerGroup")]
        public CustomerGroup CustomerGroup { get; set; }
        [JsonPropertyName("customerContact")]
        public CustomerContact PrimaryContact { get; set; }
        [JsonPropertyName("attention")]
        public CustomerContact Attention { get; set; }

        public Customer() {}
    }
}
