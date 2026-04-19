using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using AIT_ExcelAddIn_E_conomic.Configuration;
using System.Threading.Tasks;

namespace AIT_ExcelAddIn_E_conomic.Logic
{
    public class CustomerBuilder
    {
        private readonly APIHandler API;
        public CustomerBuilder(APIHandler APIService)
        {
            API = APIService;
        }

        public async Task<Customer> BuildCustomerAsync(int CustomerNumber)
        {
            Customer Customer = await API.GetCustomer(CustomerNumber);
            if (Customer is null) return null;
            else
            {
                Customer.CustomerGroup = await API.GetCustomerGroup(Customer.CustomerGroup.CustomerGroupNumber);

                if (Customer.Layout is null) Customer.Layout = Customer.CustomerGroup.Layout ?? (Layout)Settings.InvSettings["Layout"];
                if (Customer.PaymentTerms is null) Customer.PaymentTerms = (PaymentTerms)Settings.InvSettings["PaymentTerms"];
                if (Customer.VatZone is null) Customer.VatZone = (VatZone)Settings.InvSettings["VatZone"];

                return Customer;
            }
        }
    }
}
