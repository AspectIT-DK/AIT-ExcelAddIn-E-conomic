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
    public class APIHandler
    {
        internal HttpClient _httpclient;
        private JsonSerializerOptions _options;
        private const string _contentType = "application/json"; // application/json; charset=UTF-8

        public APIHandler()
        {
            _httpclient = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            _httpclient.DefaultRequestHeaders.Add("X-AppSecretToken", Config::Settings.API["X-AppSecretToken"].ToString());
            _httpclient.DefaultRequestHeaders.Add("X-AgreementGrantToken", Config::Settings.API["X-AgreementGrantToken"].ToString());
            _httpclient.BaseAddress = new Uri("https://restapi.e-conomic.com");

            _options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

        }
        public async Task<HttpResponseMessage> TestAPIConnection()
        {
            string endpoint = "/app-settings";
            var response = await _httpclient.GetAsync(endpoint);

            return response;
        }
        public async Task<HttpResponseMessage> CreateInvoiceDraft(Invoice invoice)
        {
            string endpoint = "/invoices/drafts";

            string invoicejson = JsonSerializer.Serialize<Invoice>(invoice, _options);
            Logger.WriteLine(invoicejson);
            var content = new StringContent(invoicejson, Encoding.UTF8, _contentType);
            var response = await _httpclient.PostAsync(endpoint, content);

            return response;
        }
        public async Task<LayoutCollection> GetAllLayouts()
        {
            string endpoint = "/layouts?pagesize=200";
            var response = await _httpclient.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<LayoutCollection>(content);
        }
        public async Task<VatZoneCollection> GetAllVatZones()
        {
            string endpoint = "/vat-zones?pagesize=200";
            var response = await _httpclient.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<VatZoneCollection>(content);
        }
        public async Task<PaymentTermsCollection> GetAllPaymentTerms()
        {
            string endpoint = "/payment-terms?pagesize=200";
            var response = await _httpclient.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PaymentTermsCollection>(content);
        }
    }
}
