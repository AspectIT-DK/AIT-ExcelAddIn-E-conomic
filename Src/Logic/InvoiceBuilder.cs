using AIT_ExcelAddIn_E_conomic.Configuration;
using AIT_ExcelAddIn_E_conomic.DataAccess;
using AIT_ExcelAddIn_E_conomic.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class InvoiceBuilder
    {
        private readonly APIHandler API;
        private readonly CustomerBuilder CustomerBuilder;
        public InvoiceBuilder(APIHandler APIService)
        {
            API = APIService;
            CustomerBuilder = new CustomerBuilder(APIService);
        }
        public async Task<bool> SendInvoicesToAPI(PreparedInvoiceData PreparedInvoiceData)
        {
            if (PreparedInvoiceData is null) return false;
            foreach (Invoice Invoice in PreparedInvoiceData.ConsolidatedInvoicesByCustomerNumber.Values)
            {
                var Response = await API.CreateInvoiceDraft(Invoice);
                if(!Response.IsSuccessStatusCode)
                {
                    MarkRowsAsInvoiceFail(PreparedInvoiceData, Invoice.Customer.CustomerNumber);
                    return false;
                }
                MarkRowsAsInvoiceSuccess(PreparedInvoiceData, Invoice.Customer.CustomerNumber);
            }
            return true;
        }
        private void MarkRowsAsInvoiceFail(PreparedInvoiceData PreparedInvoiceData, int AffectedCustomerNumber)
        {
            foreach(int RowNumber in PreparedInvoiceData.AssociatedRowsByCustomerNumber[AffectedCustomerNumber])
            {
                Validate.MarkRowInvoiceFail(RowNumber);
            }
        }
        private void MarkRowAsInvoiceFail(int RowNumber)
        {
            Validate.MarkRowInvoiceFail(RowNumber);
        }
        private void MarkRowsAsInvoiceSuccess(PreparedInvoiceData PreparedInvoiceData, int AffectedCustomerNumber)
        {
            foreach (int RowNumber in PreparedInvoiceData.AssociatedRowsByCustomerNumber[AffectedCustomerNumber])
            {
                Validate.MarkRowInvoiceSuccess(RowNumber);
            }
        }
        private class InvoiceRowObject : IEquatable<InvoiceRowObject>
        {
            public int RowNumber { get; set; }
            public Invoice Invoice { get; set; }
            public InvoiceLine Line { get; set; }
            public bool Equals(InvoiceRowObject other)
            {
                if (Object.ReferenceEquals(this, other)) return true;
                if (this.RowNumber == other.RowNumber) return true;
                else return false;
            }
        }
        public class PreparedInvoiceData
        {
            public Dictionary<int, Invoice> ConsolidatedInvoicesByCustomerNumber { get; set; }
            public Dictionary<int, List<int>> AssociatedRowsByCustomerNumber { get; set; }
        }

        /*
	    * ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	    *  Primary Builder Method
	    * ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
	    */
        public PreparedInvoiceData BuildInvoicesFromSelection(Excel.Range SelectedRows)
        {
            if (SelectedRows.Count == 0) { return null; } // User has no rows selected; Commit die.

            List<InvoiceRowObject> InvoiceRowObjects = new List<InvoiceRowObject>();
            Dictionary<int, Invoice> ConsolidatedInvoicesByCustomerNumber = new Dictionary<int, Invoice>();
            Dictionary<int, List<int>> AssociatedRowsByCustomerNumber = new Dictionary<int, List<int>>();

            // Build Invoice and InvoiceLine bases from selection. Ensure datatype integrity and report datatype errors to user.
            foreach (Excel.Range Row in SelectedRows)
            {
                Invoice InvoiceFromRow;
                InvoiceLine InvoiceLineFromRow;

                if ((InvoiceFromRow = BuildInvoiceFromSingleRowAsync(Row).Result) is null || (InvoiceLineFromRow = BuildInvoiceLineFromSingleRow(Row)) is null)
                {
                    MarkRowAsInvoiceFail(Row.Row);
                    Validate.ShowError($"Error on Row: {Row.Row}");
                    return null;
                }
                else
                {
                    InvoiceRowObjects.Add(new InvoiceRowObject { Invoice = InvoiceFromRow, Line = InvoiceLineFromRow, RowNumber = Row.Row });
                }
            }

            // Consolidate InvoiceRowObjects into a single Invoice base, with multiple? Invoice Line Items.
            foreach (InvoiceRowObject Object in InvoiceRowObjects)
            {
                Invoice CurrentInvoice;
                int CurrentCustomerNumber = Object.Invoice.Customer.CustomerNumber;
                if (ConsolidatedInvoicesByCustomerNumber.TryGetValue(CurrentCustomerNumber, out CurrentInvoice))
                {
                    // CASE: Invoice with CustomerNumber already exists; Consolidate Invoice Lines on this Invoice base.
                    CurrentInvoice.Lines.Add(Object.Line);
                    CurrentInvoice.Lines.Add(InvoiceLine.GetLineSeparator());
                    AssociatedRowsByCustomerNumber[CurrentCustomerNumber].Add(Object.RowNumber);
                }
                else
                {
                    // CASE: Invoice with CustomerNumber doesn't exist; Add Invoice as Base, and include current Invoice Line
                    ConsolidatedInvoicesByCustomerNumber.Add(CurrentCustomerNumber, Object.Invoice);
                    ConsolidatedInvoicesByCustomerNumber[CurrentCustomerNumber].Lines.Add(Object.Line);
                    ConsolidatedInvoicesByCustomerNumber[CurrentCustomerNumber].Lines.Add(InvoiceLine.GetLineSeparator());
                    AssociatedRowsByCustomerNumber.Add(CurrentCustomerNumber, new List<int>());
                    AssociatedRowsByCustomerNumber[CurrentCustomerNumber].Add(Object.RowNumber);
                }
            }

            // LineNumber and SortKey must be unique, per invoice. 
            foreach (KeyValuePair<int, Invoice> Invoice in ConsolidatedInvoicesByCustomerNumber)
            {
                int Counter = 1; // Starts at 1.      Per E-Conomic API Documentation.
                foreach (InvoiceLine Line in Invoice.Value.Lines)
                {
                    Line.LineNumber = Counter;
                    Line.SortKey = Counter;
                    Counter++;
                }
            }

            return (new PreparedInvoiceData { AssociatedRowsByCustomerNumber = AssociatedRowsByCustomerNumber, 
                                              ConsolidatedInvoicesByCustomerNumber = ConsolidatedInvoicesByCustomerNumber });
        }
        public async Task<Invoice> BuildInvoiceFromSingleRowAsync(Excel.Range SingleRow)
        {
            Invoice Invoice = new Invoice();
            int CustomerNumber;            
            if(!Validate.ParseInt(Convert.ToString((SingleRow.Cells[1, Settings.FieldMap["ColDefCustomerNumber"]] as Excel.Range).Value), out CustomerNumber)) { return null; }
            
            Customer Customer = await CustomerBuilder.BuildCustomerAsync(CustomerNumber);
            if (Customer is null)
            {
                Validate.ShowError($"Customer with Number: {CustomerNumber} does not exist in E-Conomic");
                return null;
            }

            string CustomerName  = Convert.ToString((SingleRow.Cells[1, Settings.FieldMap["ColDefCustomerName"]] as Excel.Range).Value);
            Invoice.Customer     = Customer;
            Invoice.Recipient    = new Recipient(Customer);
            Invoice.Layout       = Customer.Layout;
            Invoice.PaymentTerms = Customer.PaymentTerms;
            Invoice.Date         = Settings.InvoiceIssueDate.ToString("yyyy-MM-dd");

            return Invoice;
        }
        public InvoiceLine BuildInvoiceLineFromSingleRow(Excel.Range SingleRow)
        {
            string DescriptionDefinition = Settings.FieldMap["ColDefDescription"];
            decimal UnitNetPrice;
            if (!Validate.ParseDecimal(Convert.ToString((SingleRow.Cells[1, Settings.FieldMap["ColDefLineItemPrice"]] as Excel.Range).Value), out UnitNetPrice))
            {
                return null;
            }
            string Description = GetParsedDescription(DescriptionDefinition, SingleRow);

            InvoiceLine Line = new InvoiceLine();

            Line.Description    = Description;
            Line.UnitNetPrice   = UnitNetPrice;
            Line.SortKey        = 99; // 99 Magic number to show we need to change it
            Line.LineNumber     = 99; // 99 Magic number to show we need to change it
            Line.Quantity       = 1.0M;
            Line.Product        = (Product)Settings.InvSettings["Product"];
            Line.Unit           = (Unit)Settings.InvSettings["Unit"];

            return Line;
        }
        public string GetParsedDescription(string DescriptionDefinition, Excel.Range SingleRow)
        {
            List<string> Columns = new List<string>();

            string TokenPattern = "{(\\w)}";
            Regex Regex = new Regex(TokenPattern, RegexOptions.IgnoreCase);
            MatchCollection Matches = Regex.Matches(DescriptionDefinition);
            foreach (Match Match in Matches)
            {
                Group Group = Match.Groups[1];
                Columns.Add(Group.Value);
            }

            Dictionary<string, string> ReplacementMap = new Dictionary<string, string>();
            foreach (string Column in Columns)
            {
                ReplacementMap.Add(Column, Convert.ToString((SingleRow.Cells[1, Column] as Excel.Range).Value));
                //Logger.WriteLine($"Col: {Column}");
                //Logger.WriteLine($"Val: {Convert.ToString((SingleRow.Cells[1, Column] as Excel.Range).Value)}");
            }

            StringBuilder StringBuilder = new StringBuilder();
            StringBuilder.Append(DescriptionDefinition);

            foreach (string Key in ReplacementMap.Keys)
            {
                StringBuilder.Replace($"{{{Key}}}", ReplacementMap[Key]);
            }

            return StringBuilder.ToString();
        }
        public static Invoice GetTestInvoice()
        {
            Invoice inv = new Invoice();
            inv.Customer = new Customer { CustomerNumber = 1 };
            inv.Recipient = new Recipient(inv.Customer);
            //inv.Recipient = new Recipient { Name = "Recipient Name", VatZone = new VatZone() { Name = "VatZone Name", VatZoneNumber = 1 } };
            inv.Layout = new Layout { LayoutNumber = 21 };
            inv.PaymentTerms = new PaymentTerms { Name = "Payment Stinkterms", PaymentTermsNumber = 5 };

            InvoiceLine invline = new InvoiceLine();
            invline.LineNumber = 1;
            invline.Quantity = 1m;
            invline.SortKey = 1;
            invline.UnitNetPrice = 250m;
            invline.Description = "Description of Line";
            invline.Unit = new Unit { UnitNumber = 1 };
            invline.Product = new Product { ProductNumber = "1" };
            inv.Lines.Add(invline);

            return inv;
        }
    }
}
