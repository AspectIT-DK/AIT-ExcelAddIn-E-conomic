using System.Reflection;
using System.Text;
using System.Windows;

namespace AIT_ExcelAddIn_E_conomic.Views
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();

            Assembly Assembly = Assembly.GetCallingAssembly();
            StringBuilder StringBuilder = new StringBuilder();

            // Author, License and Version
            object[] customAttributes = Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            StringBuilder.AppendLine($"{((AssemblyProductAttribute)customAttributes[0]).Product}");
            customAttributes = Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            StringBuilder.AppendLine($"Author: {((AssemblyCompanyAttribute)customAttributes[0]).Company}");
            StringBuilder.AppendLine($"License: MIT License");
            StringBuilder.AppendLine($"Add-In Version: {Assembly.GetName().Version.ToString()}");

            TextBlockAssemblyInfo.Text = StringBuilder.ToString();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(LinkGithub.NavigateUri.ToString());
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(LinkAPI.NavigateUri.ToString());
        }
    }
}
