using AIT_ExcelAddIn_E_conomic.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIT_ExcelAddIn_E_conomic.DataAccess
{
    public interface IAPIHandler
    {
        Task<HttpResponseMessage> CreateInvoiceDraft(Invoice invoice);
        Task<HttpResponseMessage> TestAPIConnection();
        Task<LayoutCollection> GetAllLayouts();
        Task<VatZoneCollection> GetAllVatZones();
        Task<PaymentTermsCollection> GetAllPaymentTerms();
        Task<Customer> GetCustomer(int CustomerNumber);
        Task<CustomerGroup> GetCustomerGroup(int CustomerGroupNumber);
        Task<ProductCollection> GetAllProducts();
        Task<UnitCollection> GetAllUnits();
    }
}
