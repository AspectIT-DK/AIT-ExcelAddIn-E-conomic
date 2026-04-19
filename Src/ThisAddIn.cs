using AIT_ExcelAddIn_E_conomic.Configuration;
using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AIT_ExcelAddIn_E_conomic
{
    public static class ServiceLocator
    {
        public static IServiceProvider ServiceProvider { get; set; }
    }
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Settings.LoadSettingsFromRegistry();

            ServiceCollection Services = new ServiceCollection();
            Services.AddTransient<IAPIHandler, APIHandler>();
            ServiceLocator.ServiceProvider = Services.BuildServiceProvider();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
