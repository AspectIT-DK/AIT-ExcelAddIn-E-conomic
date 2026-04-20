using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    /*
	* ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	*  Documentation: https://restapi.e-conomic.com/schema/customers.customerNumber.contacts.get.schema.json
	* ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	*/
    public class CustomerContact
    {
        [JsonPropertyName("customerContactNumber")]
        public int CustomerContactNumber { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("deleted")]
        public bool IsDeleted { get; set; }
        [JsonPropertyName("sortKey")]
        public int SortKey { get; set; }
        [JsonPropertyName("customer")]
        public Customer ParentCustomer { get; set; }
    }
}
