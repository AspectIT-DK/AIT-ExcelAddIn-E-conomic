namespace AIT_ExcelAddIn_E_conomic
{
    partial class E_ConomicRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public E_ConomicRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab_economic = this.Factory.CreateRibbonTab();
            this.GrpInvoicing = this.Factory.CreateRibbonGroup();
            this.BtnNewInvoiceDraft = this.Factory.CreateRibbonButton();
            this.GrpSettings = this.Factory.CreateRibbonGroup();
            this.MenuBtnSettings = this.Factory.CreateRibbonMenu();
            this.BtnOpenAPISettings = this.Factory.CreateRibbonButton();
            this.BtnOpenInvoiceSettings = this.Factory.CreateRibbonButton();
            this.BtnOpenHelp = this.Factory.CreateRibbonButton();
            this.BtnOpenPreferences = this.Factory.CreateRibbonButton();
            this.tab_economic.SuspendLayout();
            this.GrpInvoicing.SuspendLayout();
            this.GrpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_economic
            // 
            this.tab_economic.Groups.Add(this.GrpInvoicing);
            this.tab_economic.Groups.Add(this.GrpSettings);
            this.tab_economic.Label = "E-Conomic";
            this.tab_economic.Name = "tab_economic";
            // 
            // GrpInvoicing
            // 
            this.GrpInvoicing.Items.Add(this.BtnNewInvoiceDraft);
            this.GrpInvoicing.Label = "Invoicing";
            this.GrpInvoicing.Name = "GrpInvoicing";
            // 
            // BtnNewInvoiceDraft
            // 
            this.BtnNewInvoiceDraft.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.BtnNewInvoiceDraft.Label = "New Invoice Draft";
            this.BtnNewInvoiceDraft.Name = "BtnNewInvoiceDraft";
            this.BtnNewInvoiceDraft.OfficeImageId = "DataFormAddRecord";
            this.BtnNewInvoiceDraft.ScreenTip = "New Invoice Draft is created using the selected Rows";
            this.BtnNewInvoiceDraft.ShowImage = true;
            this.BtnNewInvoiceDraft.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BtnNewInvoiceDraft_Click);
            // 
            // GrpSettings
            // 
            this.GrpSettings.Items.Add(this.MenuBtnSettings);
            this.GrpSettings.Label = "Settings";
            this.GrpSettings.Name = "GrpSettings";
            // 
            // MenuBtnSettings
            // 
            this.MenuBtnSettings.Items.Add(this.BtnOpenAPISettings);
            this.MenuBtnSettings.Items.Add(this.BtnOpenInvoiceSettings);
            this.MenuBtnSettings.Items.Add(this.BtnOpenPreferences);
            this.MenuBtnSettings.Items.Add(this.BtnOpenHelp);
            this.MenuBtnSettings.Label = "Settings";
            this.MenuBtnSettings.Name = "MenuBtnSettings";
            this.MenuBtnSettings.OfficeImageId = "OmsViewAccountSetting";
            this.MenuBtnSettings.ShowImage = true;
            // 
            // BtnOpenAPISettings
            // 
            this.BtnOpenAPISettings.Label = "API Settings";
            this.BtnOpenAPISettings.Name = "BtnOpenAPISettings";
            this.BtnOpenAPISettings.OfficeImageId = "ServerConnection";
            this.BtnOpenAPISettings.ScreenTip = "Open API Settings dialog";
            this.BtnOpenAPISettings.ShowImage = true;
            this.BtnOpenAPISettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BtnOpenAPISettings_Click);
            // 
            // BtnOpenInvoiceSettings
            // 
            this.BtnOpenInvoiceSettings.Label = "Invoice Settings";
            this.BtnOpenInvoiceSettings.Name = "BtnOpenInvoiceSettings";
            this.BtnOpenInvoiceSettings.OfficeImageId = "GroupMailMergeWriteInsertFields";
            this.BtnOpenInvoiceSettings.ScreenTip = "Open Invoice Settings dialog";
            this.BtnOpenInvoiceSettings.ShowImage = true;
            this.BtnOpenInvoiceSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BtnOpenInvoiceSettings_Click);
            // 
            // BtnOpenHelp
            // 
            this.BtnOpenHelp.Label = "Help";
            this.BtnOpenHelp.Name = "BtnOpenHelp";
            this.BtnOpenHelp.OfficeImageId = "Help";
            this.BtnOpenHelp.ScreenTip = "Open Help dialog";
            this.BtnOpenHelp.ShowImage = true;
            this.BtnOpenHelp.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BtnOpenHelp_Click);
            // 
            // BtnOpenPreferences
            // 
            this.BtnOpenPreferences.Label = "Preferences";
            this.BtnOpenPreferences.Name = "BtnOpenPreferences";
            this.BtnOpenPreferences.OfficeImageId = "PageOptionsDialog";
            this.BtnOpenPreferences.ScreenTip = "Open User Preferences dialog";
            this.BtnOpenPreferences.ShowImage = true;
            this.BtnOpenPreferences.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.BtnOpenPreferences_Click);
            // 
            // E_ConomicRibbon
            // 
            this.Name = "E_ConomicRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab_economic);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.E_ConomicRibbon_Load);
            this.tab_economic.ResumeLayout(false);
            this.tab_economic.PerformLayout();
            this.GrpInvoicing.ResumeLayout(false);
            this.GrpInvoicing.PerformLayout();
            this.GrpSettings.ResumeLayout(false);
            this.GrpSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab_economic;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GrpInvoicing;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup GrpSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonMenu MenuBtnSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnNewInvoiceDraft;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnOpenAPISettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnOpenInvoiceSettings;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnOpenHelp;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton BtnOpenPreferences;
    }

    partial class ThisRibbonCollection
    {
        internal E_ConomicRibbon E_ConomicRibbon
        {
            get { return this.GetRibbon<E_ConomicRibbon>(); }
        }
    }
}
