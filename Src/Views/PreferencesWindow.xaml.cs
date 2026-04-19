using AIT_ExcelAddIn_E_conomic.Configuration;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace AIT_ExcelAddIn_E_conomic.Views
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {
        public PreferencesWindow()
        {
            InitializeComponent();

            RectColorIndicatorSuccess.Fill         = new SolidColorBrush(ConvertDrawingColorToMediaColor(Settings.RowColorSettings.RowColorSuccessOperation));
            RectColorIndicatorFail.Fill            = new SolidColorBrush(ConvertDrawingColorToMediaColor(Settings.RowColorSettings.RowColorFailOperation));
            RectColorIndicatorInvoiceSuccess.Fill  = new SolidColorBrush(ConvertDrawingColorToMediaColor(Settings.RowColorSettings.RowColorInvoiceSuccess));
            RectColorIndicatorInvoiceFail.Fill     = new SolidColorBrush(ConvertDrawingColorToMediaColor(Settings.RowColorSettings.RowColorInvoiceFail));
            CheckBoxAllowRowsChangeColor.IsChecked = Settings.RowColorSettings.AllowRowsToBeColored;
        }

        private void GetColorFromDialog(System.Windows.Shapes.Rectangle Rectangle)
        {
            System.Windows.Forms.ColorDialog Dialog = new System.Windows.Forms.ColorDialog();
            if (Dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Rectangle.Fill = new SolidColorBrush(ConvertDrawingColorToMediaColor(Dialog.Color));
            }
        }

        private void BtnChangeColorInvoiceSuccess_Click(object sender, RoutedEventArgs e)
        {
            GetColorFromDialog(RectColorIndicatorInvoiceSuccess);
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Settings.RowColorSettings.RowColorSuccessOperation = ConvertMediaColorToDrawingColor(((SolidColorBrush)RectColorIndicatorSuccess.Fill).Color);
            Settings.RowColorSettings.RowColorFailOperation    = ConvertMediaColorToDrawingColor(((SolidColorBrush)RectColorIndicatorFail.Fill).Color);
            Settings.RowColorSettings.RowColorInvoiceSuccess   = ConvertMediaColorToDrawingColor(((SolidColorBrush)RectColorIndicatorInvoiceSuccess.Fill).Color);
            Settings.RowColorSettings.RowColorInvoiceFail      = ConvertMediaColorToDrawingColor(((SolidColorBrush)RectColorIndicatorInvoiceFail.Fill).Color);
            Settings.RowColorSettings.AllowRowsToBeColored     = (bool)CheckBoxAllowRowsChangeColor.IsChecked;
            Settings.SaveSettingsToRegistry();
            TextBlockStatusBar.Text = "Saved";
        }
        private void BtnChangeColorInvoiceFail_Click(object sender, RoutedEventArgs e)
        {
            GetColorFromDialog(RectColorIndicatorInvoiceFail);
        }
        private void BtnChangeColorSuccess_Click(object sender, RoutedEventArgs e)
        {
            //GetColorFromDialog(RectColorIndicatorSuccess);
        }
        private void BtnChangeColorFail_Click(object sender, RoutedEventArgs e)
        {
            //GetColorFromDialog(RectColorIndicatorFail);
        }

        // Helpers
        private System.Windows.Media.Color ConvertDrawingColorToMediaColor(System.Drawing.Color SDColor)
        {
            return new System.Windows.Media.Color { A = SDColor.A, R = SDColor.R, G = SDColor.G, B = SDColor.B };
        }
        private System.Drawing.Color ConvertMediaColorToDrawingColor(System.Windows.Media.Color WMColor)
        {
            return System.Drawing.Color.FromArgb(WMColor.A, WMColor.R, WMColor.G, WMColor.B);
        }
    }
}
