using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsightInvoice.Tests
{
    [TestClass]
    public class TaxationTests
    {

        [TestMethod]
        public void Tax_None()
        {
            // Bottle of 1 ltr. 
            var packaging = new Packaging { Type = "1 Ltr Bottle", PackQty = 1000 /*ml*/ };
            //price is 10 for every 100 ml
            var product = new Product { Name = "Cooking Oil", Price = 10, PricePer = 100 /*ml*/ };
            var scan = new InvoiceItem { Product = product, Quantity = 1, Packaging = packaging };
            scan.Product.Category = new TaxCategory { Name = "No Taxes" };
            Assert.AreEqual(scan.GrossPrice, 100m);
            Assert.AreEqual(scan.Taxes, 0m);
            Assert.AreEqual(scan.NetPrice, 100m);
        }

        [TestMethod]
        public void Tax_Multiple()
        {
            var product = new Product { Name = "Chair", Price = 100 };
            var scan = new InvoiceItem { Product = product, Quantity = 1, Packaging = null };
            scan.Product.Category = new TaxCategory { Name = "Multiple Taxes" };
            scan.Product.Category.Taxes.Add(new Tax { Percentage = 12.25m });
            scan.Product.Category.Taxes.Add(new Tax { Percentage = 5.5m });
            scan.Product.Category.Taxes.Add(new Tax { Percentage = 1m });
            Assert.AreEqual(scan.GrossPrice, 100m);
            Assert.AreEqual(scan.Taxes, 18.75m);
            Assert.AreEqual(scan.NetPrice, 118.75m);
        }

        [TestMethod]
        public void Tax_Rounding()
        {
            var product = new Product { Name = "Chair", Price = 12.49m };
            var scan = new InvoiceItem { Product = product, Quantity = 1, Packaging = null };
            scan.Product.Category = new TaxCategory { Name = "Single Tax" };
            scan.Product.Category.Taxes.Add(new Tax { Percentage = 5m });
            Assert.AreEqual(scan.GrossPrice, 12.49m);
            Assert.AreEqual(scan.Taxes, 0.65m);
            Assert.AreEqual(scan.NetPrice, 13.14m);
        }
    }
}