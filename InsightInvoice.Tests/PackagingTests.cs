using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsightInvoice.Tests
{
    [TestClass]
    public class PackagingTests
    {
        [TestMethod]
        public void PackageQty_MinDefault()
        {
            var packaging = new Packaging { Type = "One Piece" };
            //price is Rs. 10 per piece of item
            var product = new Product { Name = "Watermelon", Price = 10};
            var scan = new InvoiceItem { Product = product, Quantity = 2, Packaging = packaging };
            Assert.AreEqual(scan.GrossPrice, 20m);
        }

        [TestMethod]
        public void PackageQty_MinDozen()
        {
            // pack of 12 items sold together
            var packaging = new Packaging { Type = "Box of Dozen", PackQty = 12 };
            //price is Rs. 10 per piece of item in qty
            var product = new Product { Name = "Bananas", Price = 10  };
            var scan = new InvoiceItem { Product = product, Quantity = 2, Packaging = packaging };
            Assert.AreEqual(scan.GrossPrice, 240m);
        }

        [TestMethod]
        public void PackageQty_MinLtr()
        {
            // Bottle of 1 ltr. 
            var packaging = new Packaging { Type = "1 Ltr Bottle", PackQty = 1000 /*ml*/ };
            //price is 10 for 100 ml
            var product = new Product { Name = "Cooking Oil", Price = 10, PricePer = 100 /*ml*/ };
            var scan = new InvoiceItem { Product = product, Quantity = 1 , Packaging = packaging };
            Assert.AreEqual(scan.GrossPrice, 100m);
        }

        [TestMethod]
        public void Package_None()
        {
            //product without fixed packaging like retail petrol
            //price is 10 for 100 ML
            var product = new Product { Name = "Petrol", Price = 10, PricePer = 100 /*ml*/ };
            var scan = new InvoiceItem { Product = product, Quantity = 200, Packaging = null };
            Assert.AreEqual(scan.GrossPrice, 20m);
        }
    }
}
