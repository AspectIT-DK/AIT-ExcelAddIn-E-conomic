using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class Invoice : IEqualityComparer<Invoice>, IEquatable<Invoice>
    {
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }
        [JsonPropertyName("recipient")]
        public Recipient Recipient { get; set; }
        [JsonPropertyName("layout")]
        public Layout Layout { get; set; }
        [JsonPropertyName("paymentTerms")]
        public PaymentTerms PaymentTerms { get; set; }
        [JsonPropertyName("currency")]
        public string CurrencyCode { get; set; } = "DKK";                         // ISO-4217    // TODO: Implement Currency selector
        [JsonPropertyName("date")]
        public string Date { get; set; } = DateTime.Today.ToString("yyyy-MM-dd"); // ISO-8601
        [JsonPropertyName("lines")]
        public List<InvoiceLine> Lines { get; set; }

        public Invoice()
        {
            Lines = new List<InvoiceLine>();
        }

        public bool CustomerNumberEquals(Invoice other)
        {
            if (this.Customer.CustomerNumber == other.Customer.CustomerNumber) return true;
            else return false;
        }

        public bool Equals(Invoice x, Invoice y) // REFACTOR
        {
            if(ReferenceEquals(x,y)) return true;
            if(ReferenceEquals(x,null)) return false;
            if(ReferenceEquals(y,null)) return false;
            if(x.GetType() != y.GetType()) return false;
            return x.Customer.CustomerNumber.Equals(y.Customer.CustomerNumber);
        }
        public int GetHashCode(Invoice obj) // REMOVE
        {
            if(!(obj.Customer is null))
            {
                return obj.Customer.CustomerNumber.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }
        public bool Equals(Invoice other) // REMOVE
        {
            return this.Customer.CustomerNumber.Equals(other.Customer.CustomerNumber);
        }
    }
}
