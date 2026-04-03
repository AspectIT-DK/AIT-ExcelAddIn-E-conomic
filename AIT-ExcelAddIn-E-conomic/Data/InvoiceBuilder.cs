using AIT_ExcelAddIn_E_conomic.Configuration;
using AIT_ExcelAddIn_E_conomic.Data;
using AIT_ExcelAddIn_E_conomic.Logging;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;


namespace AIT_ExcelAddIn_E_conomic.Data
{
    public class InvoiceBuilder
    {
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

        public List<Invoice> BuildInvoicesFromSelection(Excel.Range SelectedRows)
        {
            Dictionary<int, Invoice> ConsolidatedInvoicesByCustomerNumber = new Dictionary<int, Invoice>();
            List<InvoiceRowObject> InvoiceRowObjects = new List<InvoiceRowObject>();

            // User has no rows selected, but still presses button? Commit die.
            if (SelectedRows.Count == 0) { return null; }

            // Build Invoice and InvoiceLine bases from selection. Ensure datatype integrity and report errors to user.
            foreach (Excel.Range Row in SelectedRows)
            {
                Invoice InvoiceFromRow;
                InvoiceLine InvoiceLineFromRow;

                if ((InvoiceFromRow = BuildInvoiceFromSingleRow(Row)) is null || (InvoiceLineFromRow = BuildInvoiceLineFromSingleRow(Row)) is null)
                {
                    MessageBox.Show($"Error on Row: {Row.Row}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                else
                {
                    InvoiceRowObjects.Add(new InvoiceRowObject { Invoice = InvoiceFromRow, Line = InvoiceLineFromRow, RowNumber = Row.Row });
                }
            }

            // Consolidate InvoiceRowObjects into a single Invoice base, with multiple? Invoice Line Items.
            foreach(InvoiceRowObject Object in InvoiceRowObjects)
            {
                Invoice CurrentInvoice;
                int CurrentCustomerNumber = Object.Invoice.Customer.CustomerNumber;
                if (ConsolidatedInvoicesByCustomerNumber.TryGetValue(CurrentCustomerNumber, out CurrentInvoice) == true)
                {
                    // CASE: Invoice with CustomerNumber already exists; Consolidate Invoice Lines on this Invoice base.
                    CurrentInvoice.Lines.Add(Object.Line);
                }
                else
                {
                    // CASE: Invoice with CustomerNumber doesn't exist; Add Invoice as Base, and include current Invoice Line
                    ConsolidatedInvoicesByCustomerNumber.Add(CurrentCustomerNumber, Object.Invoice);
                    ConsolidatedInvoicesByCustomerNumber[CurrentCustomerNumber].Lines.Add(Object.Line);
                }
            }

            // REFACTOR
            // LineNumber and SortKey must be unique, per invoice. 
            foreach(KeyValuePair<int, Invoice> Invoice in ConsolidatedInvoicesByCustomerNumber)
            {
                int Counter = 1; // Starts at 1.      Per E-Conomic API Documentation.
                foreach(InvoiceLine Line in Invoice.Value.Lines)
                {
                    Line.LineNumber = Counter;
                    Line.SortKey    = Counter;
                    Counter++;
                }
            }

            return ConsolidatedInvoicesByCustomerNumber.Values.ToList<Invoice>();
        }
        public Invoice BuildInvoiceFromSingleRow(Excel.Range SingleRow)
        {
            Invoice Invoice = new Invoice();
            int CustomerNumber;
            
            if(!Validate.ParseInt(Convert.ToString((SingleRow.Cells[1, Settings.FieldMap["ColDefCustomerNumber"]] as Excel.Range).Value), out CustomerNumber))
            {
                return null;
            }

            string CustomerName = Convert.ToString((SingleRow.Cells[1, Settings.FieldMap["ColDefCustomerName"]] as Excel.Range).Value);
            Invoice.Customer = new Customer { CustomerNumber = CustomerNumber };
            Invoice.Recipient = new Recipient { Name = CustomerName, VatZone = (VatZone)Settings.InvSettings["VatZone"] };
            Invoice.Layout = (Layout)Settings.InvSettings["Layout"];
            Invoice.PaymentTerms = (PaymentTerms)Settings.InvSettings["PaymentTerms"];

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

            Line.Description = Description;
            Line.UnitNetPrice = UnitNetPrice;
            Line.SortKey = 99; // 99 Magic number to show we need to change it
            Line.LineNumber = 99; // 99 Magic number to show we need to change it
            Line.Quantity = 1.0M;
            Line.Product = new Product { ProductNumber = "1" };

            return Line;
        }
        //public Invoice BuildInvoice(Excel.Range Range)
        //{
        //    Invoice Invoice = new Invoice();
        //    List<InvoiceLine> Lines = new List<InvoiceLine>();

        //    Range = Range.EntireRow;
        //    string DescriptionDefinition = Settings.FieldMap["ColDefDescription"];

        //    string CustomerNumber = Convert.ToString((Range.Cells[1, Settings.FieldMap["ColDefCustomerNumber"]] as Excel.Range).Value);
        //    string CustomerName   = Convert.ToString((Range.Cells[1, Settings.FieldMap["ColDefCustomerName"]] as Excel.Range).Value);
        //    string UnitNetPrice   = Convert.ToString((Range.Cells[1, Settings.FieldMap["ColDefLineItemPrice"]] as Excel.Range).Value);


        //    Invoice.Customer     = new Customer { CustomerNumber = Int32.Parse(CustomerNumber) };
        //    Invoice.Recipient    = new Recipient { Name = CustomerName, VatZone = (VatZone)Settings.InvSettings["VatZone"] };
        //    Invoice.Layout       = (Layout)Settings.InvSettings["Layout"];
        //    Invoice.PaymentTerms = (PaymentTerms)Settings.InvSettings["PaymentTerms"];

        //    InvoiceLine Line = new InvoiceLine();
        //    Line.Quantity = 1;
        //    Line.LineNumber = 1;
        //    Line.SortKey = 1;
        //    Line.UnitNetPrice = Int32.Parse(UnitNetPrice);
        //    Line.Description = GetParsedDescription(DescriptionDefinition, Range);

        //    Lines.Add(Line);

        //    Invoice.Lines = Lines;

        //    return Invoice;
        //}

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
    }
}
