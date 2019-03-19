using System.Collections.Generic;

namespace InsightInvoice.Tests
{
    public class InvoiceTestSetup
    {
        public virtual Packaging BoxPackaging(int packageUnits = 1) => new Packaging { Type = "Box", PackQty = packageUnits };
        public virtual Packaging BottlePackaging(int packageUnits = 1) => new Packaging { Type = "Bottle", PackQty = packageUnits };
        public virtual Packaging PacketPackaging(int packageUnits = 1) => new Packaging { Type = "Packet", PackQty = packageUnits };
        public virtual Tax SalesTax(int taxRate = 10) => new Tax { Name = "Sales Tax", Percentage = taxRate };
        public virtual Tax ImportDuty(int duty = 5) => new Tax { Name = "Import Duty", Percentage = duty };

        public virtual TaxCategory Local()
        {
            return new TaxCategory()
            {
                Name = "Local",
                Taxes = new List<Tax> { this.SalesTax() }
            };
        }

        public virtual TaxCategory Import()
        {
            return new TaxCategory()
            {
                Name = "Imported",
                Taxes = new List<Tax> { this.SalesTax(), this.ImportDuty() }
            };
        }
        public TaxCategory LocalSalesTaxExempt()
        {
            return new TaxCategory()
            {
                Name = "ST Exempt",
            };
        }
        public TaxCategory ImportSalesTaxExempt()
        {
            return new TaxCategory()
            {
                Name = "ST Exempt",
                Taxes = new List<Tax> { this.ImportDuty() }
            };
        }

        public InvoiceItem Scan(string productName, decimal price, decimal quantity, TaxCategory category, Packaging packaging)
        {
            return new InvoiceItem
            {
                Quantity = quantity,
                Product = new Product { Name = productName, Price = price, Category = category},
                Packaging = packaging
            };
        }
    }
}
