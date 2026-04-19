using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Config = AIT_ExcelAddIn_E_conomic.Configuration;

namespace AIT_ExcelAddIn_E_conomic.DataAccess
{
    //
    // Documentation: https://restdocs.e-conomic.com/
    // Default Page Size for requests is 1000
    //
    public class APIHandler
    {
        private HttpClient HttpClient;
        private JsonSerializerOptions Options;
        private const string ContentType = "application/json"; // application/json; charset=UTF-8

        public APIHandler()
        {
            HttpClient = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            HttpClient.DefaultRequestHeaders.Add("X-AppSecretToken", Config::Settings.API["X-AppSecretToken"].ToString());
            HttpClient.DefaultRequestHeaders.Add("X-AgreementGrantToken", Config::Settings.API["X-AgreementGrantToken"].ToString());
            HttpClient.BaseAddress = new Uri("https://restapi.e-conomic.com");

            Options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

        }
        public async Task<HttpResponseMessage> CreateInvoiceDraft(Invoice invoice)
        {
            string Endpoint = "/invoices/drafts";

            string InvoiceJSON = JsonSerializer.Serialize<Invoice>(invoice, Options);
            //Logger.WriteLine(InvoiceJSON);
            var Content = new StringContent(InvoiceJSON, Encoding.UTF8, ContentType);
            var Response = await HttpClient.PostAsync(Endpoint, Content);

            return Response;
        }
        public async Task<HttpResponseMessage> TestAPIConnection()
        {
            string Endpoint = "/app-settings";
            var Response = await HttpClient.GetAsync(Endpoint);

            return Response;
        }

        public async Task<LayoutCollection> GetAllLayouts()
        {
            string Endpoint = "/layouts?pagesize=1000"; // TODO: Implement consistent way to actually get - ALL - Layouts; If > 1000 exist, we only get the first 1000.
            var Response = await HttpClient.GetAsync(Endpoint);
            var Content = await Response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<LayoutCollection>(Content);
        }
        public async Task<VatZoneCollection> GetAllVatZones()
        {
            string Endpoint = "/vat-zones?pagesize=1000"; // TODO: Implement consistent way to actually get - ALL - VAT Zones; If > 1000 exist, we only get the first 1000.
            var Response = await HttpClient.GetAsync(Endpoint);
            var Content = await Response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<VatZoneCollection>(Content);
        }
        public async Task<PaymentTermsCollection> GetAllPaymentTerms()
        {
            string Endpoint = "/payment-terms?pagesize=1000"; // TODO: Implement consistent way to actually get - ALL - Payment terms; If > 1000 exist, we only get the first 1000.
            var Response = await HttpClient.GetAsync(Endpoint);
            var Content = await Response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PaymentTermsCollection>(Content);
        }
        public async Task<Customer> GetCustomer(int CustomerNumber)
        {
            string Endpoint = $"/customers/{CustomerNumber}";
            var Response = await HttpClient.GetAsync(Endpoint);
            var Content = await Response.Content.ReadAsStringAsync();

            if (Response.IsSuccessStatusCode) return JsonSerializer.Deserialize<Customer>(Content);
            else return null;
        }
        public async Task<CustomerGroup> GetCustomerGroup(int CustomerGroupNumber)
        {
            string Endpoint = $"/customer-groups/{CustomerGroupNumber}";
            var Response = await HttpClient.GetAsync(Endpoint);
            var Content = await Response.Content.ReadAsStringAsync();

            if (Response.IsSuccessStatusCode) return JsonSerializer.Deserialize<CustomerGroup>(Content);
            else return null;
        }
        public async Task<ProductCollection> GetAllProducts()
        {
            string Endpoint = "/products?pagesize=1000"; // TODO: Implement consistent way to actually get - ALL - products; If > 1000 products exist, we only get the first 1000.
            var Response = await HttpClient.GetAsync(Endpoint);
            var Content = await Response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ProductCollection>(Content);
        }
        public async Task<UnitCollection> GetAllUnits()
        {
            string Endpoint = "/units?pagesize=1000"; // TODO: Implement consistent way to actually get - ALL - products; If > 1000 products exist, we only get the first 1000.
            var Response = await HttpClient.GetAsync(Endpoint);
            var Content = await Response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<UnitCollection>(Content);
        }
    }
}
