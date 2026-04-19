using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using AIT_ExcelAddIn_E_conomic.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Diagnostics;

namespace AIT_ExcelAddIn_E_conomic
{
    public partial class E_ConomicRibbon
    {
        private void E_ConomicRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            ShowDebugButtonsIfDebugging();
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
            InvoiceBuilder InvoiceBuilder = new InvoiceBuilder(new APIHandler());
            await InvoiceBuilder.SendInvoicesToAPI(InvoiceBuilder.BuildInvoicesFromSelection(ExcelHelper.GetSelectedRows()));
        }

        private void BtnOpenPreferences_Click(object sender, RibbonControlEventArgs e)
        {
            PreferencesWindow Window = new PreferencesWindow();
            Window.ShowDialog();
        }

        // ----------------
        // Debugging
        // ----------------
        [Conditional("DEBUG")]
        private void ShowDebugButtonsIfDebugging()
        {
            GrpDebug.Visible = true;
        }

        private void BtnDebug1_Click(object sender, RibbonControlEventArgs e)
        {
            APIHandler API = new APIHandler();

            Invoice Invoice = InvoiceBuilder.GetTestInvoice();
        }
    }
}
