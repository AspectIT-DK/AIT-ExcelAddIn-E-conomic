using AIT_ExcelAddIn_E_conomic.Configuration;
using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AIT_ExcelAddIn_E_conomic.Views
{
    /// <summary>
    /// Interaction logic for InvoiceSettingsWindow.xaml
    /// </summary>
    public partial class InvoiceSettingsWindow : Window
    {
        private APIHandler APIHandler;
        private InvoiceFields Fields;
        private List<InvoiceFields> InvoiceFieldList;
        private CollectionOf<Layout> Layouts;
        private CollectionOf<VatZone> VatZones;
        private CollectionOf<PaymentTerms> PaymentTerms;
        private CollectionOf<Product> Products;
        private CollectionOf<Unit> Units;
        public InvoiceSettingsWindow()
        {
            InitializeComponent();
            APIHandler = new APIHandler();
            
            // Column Mapping Datagrid
            Fields = new InvoiceFields();
            InvoiceFieldList = new List<InvoiceFields>();
            InvoiceFieldList.Add(Fields);
            DataGridInvoiceColDefinition.DataContext = Fields;
            DataGridInvoiceColDefinition.ItemsSource = InvoiceFieldList;

            // Description
            TextBoxDescriptionDefinition.DataContext = Fields;

            // Comboboxes - Default Layout, Default VAT Zone, Default Payment Terms, Default Product, Invoice Issued Date
            Layouts                         = APIHandler.GetAllLayouts().Result;
            Layout SelectedLayout           = (Layout)Settings.InvSettings["Layout"];
            ComboBoxLayouts.DataContext     = Layouts;
            ComboBoxLayouts.SelectedValue   = SelectedLayout.LayoutNumber;

            VatZones                        = APIHandler.GetAllVatZones().Result;
            VatZone SelectedVatZone         = (VatZone)Settings.InvSettings["VatZone"];
            ComboBoxVatZones.DataContext    = VatZones;
            ComboBoxVatZones.SelectedValue  = SelectedVatZone.VatZoneNumber;

            PaymentTerms                        = APIHandler.GetAllPaymentTerms().Result;
            PaymentTerms SelectedPaymentTerms   = (PaymentTerms)Settings.InvSettings["PaymentTerms"];
            ComboBoxPaymentTerms.DataContext    = PaymentTerms;
            ComboBoxPaymentTerms.SelectedValue  = SelectedPaymentTerms.PaymentTermsNumber;

            Products                    = APIHandler.GetAllProducts().Result;
            ComboBoxProduct.DataContext = Products;
            Object SelectedProduct;
            if (Settings.InvSettings.TryGetValue("Product", out SelectedProduct))
            {
                ComboBoxProduct.SelectedValue = (SelectedProduct as Product).ProductNumber;
            }

            Units                    = APIHandler.GetAllUnits().Result;
            ComboBoxUnit.DataContext = Units;
            Object SelectedUnit;
            if (Settings.InvSettings.TryGetValue("Unit", out SelectedUnit))
            {
                ComboBoxUnit.SelectedValue = (SelectedUnit as Unit).UnitNumber;
            }

            DatePickerInvoiceCreatedDate.SelectedDate = Settings.InvoiceIssueDate;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Settings.FieldMap["ColDefCustomerNumber"]    = Fields.CustomerNumber;
            Settings.FieldMap["ColDefCustomerName"]      = Fields.CustomerName;
            Settings.FieldMap["ColDefDescription"]       = Fields.Description;
            Settings.FieldMap["ColDefLineItemPrice"]     = Fields.LineItemPrice;
            Settings.FieldMap["CultureDecimalDelimiter"] = Fields.CultureDecimalDelimiter;
            Settings.InvSettings["Layout"]               = (Layout)ComboBoxLayouts.SelectedItem;
            Settings.InvSettings["PaymentTerms"]         = (PaymentTerms)ComboBoxPaymentTerms.SelectedItem;
            Settings.InvSettings["VatZone"]              = (VatZone)ComboBoxVatZones.SelectedItem;
            Settings.InvSettings["Product"]              = (Product)ComboBoxProduct.SelectedItem;
            Settings.InvSettings["Unit"]                 = (Unit)ComboBoxUnit.SelectedItem;
            Settings.InvoiceIssueDate                    = (DateTime)DatePickerInvoiceCreatedDate.SelectedDate;

            Settings.SaveSettingsToRegistry();
            //TextBlockStatusBar.Foreground = Brushes.Black;
            TextBlockStatusBar.Text = "Saved";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Do nothing; Close Window
        }

        private class InvoiceFields
        {
            public string CustomerNumber { get; set; }
            public string CustomerName { get; set; }
            public string Description { get; set; }
            public string LineItemPrice { get; set; }
            public string CultureDecimalDelimiter { get; set; }

            public InvoiceFields()
            {
                CustomerNumber  = Settings.FieldMap["ColDefCustomerNumber"];
                CustomerName    = Settings.FieldMap["ColDefCustomerName"];
                Description     = Settings.FieldMap["ColDefDescription"];
                LineItemPrice   = Settings.FieldMap["ColDefLineItemPrice"];
                CultureDecimalDelimiter = Settings.FieldMap["CultureDecimalDelimiter"];
            }
        }

        private void BtnShowHelp_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow window = new HelpWindow();
            window.ShowDialog();
        }
    }
}
