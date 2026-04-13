using AIT_ExcelAddIn_E_conomic.Configuration;
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
    /// Interaction logic for APISettingsWindow.xaml
    /// </summary>
    public partial class APISettingsWindow : Window
    {
        private APIHandler APIHandler;
        public APISettingsWindow()
        {
            InitializeComponent();
            APIHandler = new APIHandler();

            TextBoxAPIKey.Text       = Settings.API["X-AppSecretToken"];
            TextBoxAgreementKey.Text = Settings.API["X-AgreementGrantToken"];
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Settings.API["X-AppSecretToken"]      = TextBoxAPIKey.Text;
            Settings.API["X-AgreementGrantToken"] = TextBoxAgreementKey.Text;
            Settings.SaveSettingsToRegistry();
            TextBlockStatusBar.Foreground = Brushes.Black;
            TextBlockStatusBar.Text = "Saved";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Do Nothing
        }

        private async void BtnTestAPI_Click(object sender, RoutedEventArgs e)
        {
            APIHandler = new APIHandler();

            var res = await APIHandler.TestAPIConnection();
            var status = res.IsSuccessStatusCode;

            Logger.WriteLine("API Test - Status Code: " + res.StatusCode.ToString());

            if (status)
            {
                TextBlockStatusBar.Foreground = Brushes.DarkGreen;
                TextBlockStatusBar.Text = "PASSED - Status Code: " + res.StatusCode.ToString();
            }
            else
            {
                TextBlockStatusBar.Foreground = Brushes.DarkRed;
                TextBlockStatusBar.Text = "FAILED - Status Code: " + res.StatusCode.ToString();
            }
        }
    }
}
