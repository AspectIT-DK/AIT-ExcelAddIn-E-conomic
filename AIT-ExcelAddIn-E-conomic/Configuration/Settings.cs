using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using AIT_ExcelAddIn_E_conomic.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AIT_ExcelAddIn_E_conomic.Configuration
{
    public static class Settings
    {
        public static Dictionary<string, string> API         = new Dictionary<string, string>();
        public static Dictionary<string, object> InvSettings = new Dictionary<string, object>();
        public static Dictionary<string, string> FieldMap    = new Dictionary<string, string>();
        public static RowColorSettings RowColorSettings      = new RowColorSettings();

        // Transient / Session-Only Settings
        public static DateTime InvoiceIssueDate              = DateTime.Today;

        public static bool SaveSettingsToRegistry()
        {
            Logger.WriteLine("Saving Settings to Registry");

            // API Settings
            RegistryHelper.SetValue("X-AppSecretToken",      API["X-AppSecretToken"], Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("X-AgreementGrantToken", API["X-AgreementGrantToken"], Microsoft.Win32.RegistryValueKind.String);

            // Invoice Settings
            Layout Layout = (Layout)InvSettings["Layout"];
            RegistryHelper.SetValue("LayoutName",   Layout.Name, Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("LayoutNumber", Layout.LayoutNumber, Microsoft.Win32.RegistryValueKind.DWord);

            PaymentTerms PaymentTerms = (PaymentTerms)InvSettings["PaymentTerms"];
            RegistryHelper.SetValue("PaymentTermsName",   PaymentTerms.Name, Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("PaymentTermsNumber", PaymentTerms.PaymentTermsNumber, Microsoft.Win32.RegistryValueKind.DWord);

            VatZone VatZone = (VatZone)InvSettings["VatZone"];
            RegistryHelper.SetValue("VatZoneName",   VatZone.Name, Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("VatZoneNumber", VatZone.VatZoneNumber, Microsoft.Win32.RegistryValueKind.DWord);

            Product Product = (Product)InvSettings["Product"];
            RegistryHelper.SetValue("ProductName", Product.Name, Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ProductNumber", Product.ProductNumber, Microsoft.Win32.RegistryValueKind.String); // Intentionally a string

            Unit Unit = (Unit)InvSettings["Unit"];
            RegistryHelper.SetValue("UnitName", Unit.Name, Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("UnitNumber", Unit.UnitNumber, Microsoft.Win32.RegistryValueKind.DWord);

            // Field Mapping Settings
            RegistryHelper.SetValue("ColDefCustomerNumber", FieldMap["ColDefCustomerNumber"], Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ColDefCustomerName",   FieldMap["ColDefCustomerName"], Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ColDefLineItemPrice",  FieldMap["ColDefLineItemPrice"], Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ColDefDescription",    FieldMap["ColDefDescription"], Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("CultureDecimalDelimiter", FieldMap["CultureDecimalDelimiter"], Microsoft.Win32.RegistryValueKind.String);

            // Row Color Settings
            RegistryHelper.SetValue("RowColorSuccessOperation", RowColorSettings.RowColorSuccessOperation.ToArgb(), Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("RowColorFailOperation",    RowColorSettings.RowColorFailOperation.ToArgb(), Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("RowColorInvoiceSuccess",   RowColorSettings.RowColorInvoiceSuccess.ToArgb(), Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("RowColorInvoiceFail",      RowColorSettings.RowColorInvoiceFail.ToArgb(), Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("AllowRowsToBeColored",     RowColorSettings.AllowRowsToBeColored, Microsoft.Win32.RegistryValueKind.DWord);

            return true;
        }

        public static bool LoadSettingsFromRegistry()
        {
            // Is this the first time running this Add-in?
            if (RegistryHelper.ValueExists("Firstrun") is false)
            {
                Logger.WriteLine("Settings failed to load, is this first time run?");
                Settings.InitSettingsToRegistry();
                return false;
            }

            // API Settings
            API.Add("X-AppSecretToken",      RegistryHelper.GetValue<string>("X-AppSecretToken", ""));
            API.Add("X-AgreementGrantToken", RegistryHelper.GetValue<string>("X-AgreementGrantToken", ""));

            // Invoice Settings
            Layout Layout       = new Layout();
            Layout.Name         = RegistryHelper.GetValue<string>("LayoutName", "");
            Layout.LayoutNumber = RegistryHelper.GetValue<int>("LayoutNumber");

            PaymentTerms PaymentTerms       = new PaymentTerms();
            PaymentTerms.Name               = RegistryHelper.GetValue<string>("PaymentTermsName", "");
            PaymentTerms.PaymentTermsNumber = RegistryHelper.GetValue<int>("PaymentTermsNumber");

            VatZone VatZone       = new VatZone();
            VatZone.Name          = RegistryHelper.GetValue<string>("VatZoneName", "");
            VatZone.VatZoneNumber = RegistryHelper.GetValue<int>("VatZoneNumber");

            Product Product       = new Product();
            Product.Name          = RegistryHelper.GetValue<string>("ProductName", "");
            Product.ProductNumber = RegistryHelper.GetValue<string>("ProductNumber", ""); // Intentionally a string

            Unit Unit             = new Unit();
            Unit.Name             = RegistryHelper.GetValue<string>("UnitName", "");
            Unit.UnitNumber       = RegistryHelper.GetValue<int>("UnitNumber");

            InvSettings.Add("Layout", Layout);
            InvSettings.Add("PaymentTerms", PaymentTerms);
            InvSettings.Add("VatZone", VatZone);
            InvSettings.Add("Product", Product);
            InvSettings.Add("Unit", Unit);

            // Field Mapping Settings
            FieldMap.Add("ColDefCustomerNumber", RegistryHelper.GetValue<string>("ColDefCustomerNumber", ""));
            FieldMap.Add("ColDefCustomerName",   RegistryHelper.GetValue<string>("ColDefCustomerName", ""));
            FieldMap.Add("ColDefLineItemPrice",  RegistryHelper.GetValue<string>("ColDefLineItemPrice", ""));
            FieldMap.Add("ColDefDescription",    RegistryHelper.GetValue<string>("ColDefDescription", ""));
            FieldMap.Add("CultureDecimalDelimiter", RegistryHelper.GetValue<string>("CultureDecimalDelimiter", ""));

            // Row Color Settings
            RowColorSettings.RowColorSuccessOperation = Color.FromArgb(RegistryHelper.GetValue<int>("RowColorSuccessOperation"));
            RowColorSettings.RowColorFailOperation    = Color.FromArgb(RegistryHelper.GetValue<int>("RowColorFailOperation"));
            RowColorSettings.RowColorInvoiceSuccess   = Color.FromArgb(RegistryHelper.GetValue<int>("RowColorInvoiceSuccess"));
            RowColorSettings.RowColorInvoiceFail      = Color.FromArgb(RegistryHelper.GetValue<int>("RowColorInvoiceFail"));
            RowColorSettings.AllowRowsToBeColored     = RegistryHelper.GetValue<bool>("AllowRowsToBeColored");

            //Logger.WriteLine("Settings loaded");
            return true;
        }

        public static bool InitSettingsToRegistry()
        {
            Logger.WriteLine("Initializing Settings registry base key and initial subkeys");
            RegistryHelper.SetValue("Firstrun",             "", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("X-AppSecretToken",     "", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("X-AgreementGrantToken","", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("LayoutName",           "", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("LayoutNumber",         0, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("PaymentTermsName",     "", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("PaymentTermsNumber",   0, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("VatZoneName",          "", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("VatZoneNumber",        0, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("ProductName",          "", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ProductNumber",        "", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("UnitNumber",           "", Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("UnitName",             "", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ColDefCustomerNumber", "A", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ColDefCustomerName",   "B", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ColDefDescription",    "{C}", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("ColDefLineItemPrice",  "D", Microsoft.Win32.RegistryValueKind.String);
            RegistryHelper.SetValue("CultureDecimalDelimiter", ",", Microsoft.Win32.RegistryValueKind.String);

            // Row Color Settings
            const uint GreenARGB = 0xff80ff80; // 32-bit ARGB value
            const uint RedARGB   = 0xffff8080; // 32-bit ARGB value
            RegistryHelper.SetValue("RowColorSuccessOperation", GreenARGB, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("RowColorFailOperation",    RedARGB, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("RowColorInvoiceSuccess",   GreenARGB, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("RowColorInvoiceFail",      RedARGB, Microsoft.Win32.RegistryValueKind.DWord);
            RegistryHelper.SetValue("AllowRowsToBeColored",     1, Microsoft.Win32.RegistryValueKind.DWord);

            return true;
        }
    }
}
