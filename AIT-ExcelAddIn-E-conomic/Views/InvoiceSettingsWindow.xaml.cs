using AIT_ExcelAddIn_E_conomic.Configuration;
using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using AIT_ExcelAddIn_E_conomic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private LayoutCollection Layouts;
        private VatZoneCollection VatZones;
        private PaymentTermsCollection PaymentTerms;
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

            // Layout and Terms
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
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Settings.FieldMap["ColDefCustomerNumber"]   = Fields.CustomerNumber;
            Settings.FieldMap["ColDefCustomerName"]     = Fields.CustomerName;
            Settings.FieldMap["ColDefDescription"]      = Fields.Description;
            Settings.FieldMap["ColDefLineItemPrice"]    = Fields.LineItemPrice;
            Settings.FieldMap["CultureDecimalDelimiter"] = Fields.CultureDecimalDelimiter;
            Settings.InvSettings["Layout"]              = (Layout)ComboBoxLayouts.SelectedItem;
            Settings.InvSettings["PaymentTerms"]        = (PaymentTerms)ComboBoxPaymentTerms.SelectedItem;
            Settings.InvSettings["VatZone"]             = (VatZone)ComboBoxVatZones.SelectedItem;

            Settings.SaveSettingsToRegistry();
            //TextBlockStatusBar.Foreground = Brushes.Black;
            TextBlockStatusBar.Text = "Saved";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        protected class InvoiceFields
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
