using System;
using System.Collections.Generic;
using System.Linq;

namespace InsightInvoice
{
    public class Invoice
    {
        public IList<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

        public decimal GrossTotal
        {
            get
            {
                return this.Items?.Sum(a => a.GrossPrice) ?? 0;
            }
        }

        public decimal Taxes
        {
            get
            {
                return this.Items?.Sum(a => a.Taxes) ?? 0;
            }
        }

        public decimal NetTotal
        {
            get
            {
                return this.Items?.Sum(a => a.NetPrice) ?? 0;
            }
        }

        public override string ToString()
        {
            return $"{string.Join(Environment.NewLine, this.Items)}"+
                   $"{Environment.NewLine}Sales Taxes: {this.Taxes.ToString("n2")}"+
                   $"{Environment.NewLine}Total: {this.NetTotal.ToString("n2")}";
        }

        public string Verbose()
        {
            return $"{string.Join(Environment.NewLine, this.Items.Select(a=> a.Verbose()))}" +
                   $"{Environment.NewLine}Gross Total: {this.GrossTotal.ToString("n2")}" +
                   $"{Environment.NewLine}Plus Taxes: {this.Taxes.ToString("n2")}" +
                   $"{Environment.NewLine}Net Total: {this.NetTotal.ToString("n2")}";
        }
    }
}