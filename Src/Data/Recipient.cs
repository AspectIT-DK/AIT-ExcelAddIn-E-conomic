using System.Text.Json.Serialization;
using System.Windows.Media;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Recipient
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("vatZone")]
        public VatZone VatZone { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("zip")]
        public string Postcode { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("ean")]
        public string EAN { get; set; }
        //[JsonPropertyName("attention")]
        //public string Attention { get; set; }
        [JsonPropertyName("publicEntryNumber")]
        public string CompanyNumber { get; set; }
        public Recipient() {}
        public Recipient(Customer Customer)
        {
            Name     = Customer.Name;
            Address  = Customer.Address;
            City     = Customer.City;
            Postcode = Customer.Postcode;
            Country  = Customer.Country;

            VatZone       = Customer.VatZone;
            EAN           = Customer.EAN;
            CompanyNumber = Customer.CompanyNumber;
        }

    }
}
