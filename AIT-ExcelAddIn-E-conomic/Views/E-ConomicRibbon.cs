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
            APISettingsWindow Window = new APISettingsWindow();
            Window.ShowDialog();
        }

        private void BtnOpenInvoiceSettings_Click(object sender, RibbonControlEventArgs e)
        {
            InvoiceSettingsWindow Window = new InvoiceSettingsWindow();
            Window.ShowDialog();
        }

        private void BtnOpenHelp_Click(object sender, RibbonControlEventArgs e)
        {
            HelpWindow Window = new HelpWindow();
            Window.ShowDialog();
        }

        private async void BtnNewInvoiceDraft_Click(object sender, RibbonControlEventArgs e)
        {
            InvoiceBuilder InvoiceBuilder = new InvoiceBuilder();
            await InvoiceBuilder.SendInvoicesToAPI(InvoiceBuilder.BuildInvoicesFromSelection(ExcelHelper.GetSelectedRows()));
        }

        private void BtnOpenPreferences_Click(object sender, RibbonControlEventArgs e)
        {
            PreferencesWindow Window = new PreferencesWindow();
            Window.ShowDialog();
        }
    }
}
