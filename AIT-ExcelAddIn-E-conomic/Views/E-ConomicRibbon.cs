using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using AIT_ExcelAddIn_E_conomic.Logging;
using AIT_ExcelAddIn_E_conomic.Views;
using Microsoft.Office.Tools.Ribbon;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace AIT_ExcelAddIn_E_conomic
{
    public partial class E_ConomicRibbon
    {
        private void E_ConomicRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void BtnOpenAPISettings_Click(object sender, RibbonControlEventArgs e)
        {
            APISettingsWindow window = new APISettingsWindow();
            window.ShowDialog();
        }

        private void BtnOpenInvoiceSettings_Click(object sender, RibbonControlEventArgs e)
        {
            InvoiceSettingsWindow window = new InvoiceSettingsWindow();
            window.ShowDialog();
        }

        private void BtnOpenHelp_Click(object sender, RibbonControlEventArgs e)
        {
            HelpWindow window = new HelpWindow();
            window.ShowDialog();
        }

        private async void BtnNewInvoiceDraft_Click(object sender, RibbonControlEventArgs e)
        {
            InvoiceBuilder InvoiceBuilder = new InvoiceBuilder();
            APIHandler APIHandler = new APIHandler();
            List<Invoice> Invoices = InvoiceBuilder.BuildInvoicesFromSelection(ExcelHelper.GetSelectedRows());
            if(Invoices is null)
            {
                return;
            }

            foreach (Invoice Invoice in Invoices)
            {
                var Response = await APIHandler.CreateInvoiceDraft(Invoice);
                Logger.WriteLine(Invoice.Customer.CustomerNumber.ToString());
                Logger.WriteLine("API Call: " + ((int)Response.StatusCode).ToString() + " - " + Response.ReasonPhrase.ToString());
            }
        }
    }
}
