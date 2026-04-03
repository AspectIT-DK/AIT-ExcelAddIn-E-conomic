using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class VatZone
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null;
        [JsonPropertyName("vatZoneNumber")]
        public int VatZoneNumber { get; set; }
        [JsonPropertyName("enabledForCustomer")]
        public bool IsEnabledForCustomer { get; set; } = true;
        [JsonPropertyName("enabledForSupplier")]
        public bool IsEnabledForSupplier { get; set; } = true;
        [JsonPropertyName("self")]
        public string Self { get; set; }

        public override string ToString()
        {
            return ($"VatZone - Name: {Name}, Number: {VatZoneNumber}");
        }
    }
}
