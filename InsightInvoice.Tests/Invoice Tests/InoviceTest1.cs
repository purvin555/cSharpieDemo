using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsightInvoice.Tests
{
    [TestClass]
    public class InvoiceTest1
    {
        private InvoiceTestSetup setup = new InvoiceTestSetup();

        #region Book Item

        public InvoiceItem BookScan() 
            => setup.Scan("Book", price: 12.49m, quantity: 1, category: setup.LocalSalesTaxExempt(),packaging: null);

        [TestMethod]
        public void Invoice1_BookTaxes() => Assert.AreEqual(this.BookScan().Taxes, 0);

        [TestMethod]
        public void Invoice1_BookPayable() => Assert.AreEqual(this.BookScan().NetPrice, 12.49m);

        #endregion Book Item

        #region CD Item

        public InvoiceItem CDScan() 
            => setup.Scan("Music CD", price: 14.99m, quantity: 1, category: setup.Local(), packaging: null);

        [TestMethod]
        public void Invoice1_CDTaxes() => Assert.AreEqual(this.CDScan().Taxes, 1.5m);

        [TestMethod]
        public void Invoice1_CDPayable() => Assert.AreEqual(this.CDScan().NetPrice, 16.49m);

        #endregion CD Item

        #region Chocolate Item

        public InvoiceItem ChocolateScan() 
            => setup.Scan("Chocolate Bar",price: 0.85m, quantity: 1, category: setup.LocalSalesTaxExempt(), packaging: null);

        [TestMethod]
        public void Invoice1_ChocolateTaxes() => Assert.AreEqual(this.ChocolateScan().Taxes, 0);

        [TestMethod]
        public void Invoice1_ChocolatePayable() => Assert.AreEqual(this.ChocolateScan().NetPrice, 0.85m);

        #endregion Chocolate Item

        #region Invoice Of All Items

        public Invoice Invoice() 
            => new Invoice { Items = { this.BookScan(), this.CDScan(), this.ChocolateScan() } };

        [TestMethod]
        public void Invoice1_Taxes() => Assert.AreEqual(this.Invoice().Taxes, 1.5m);

        [TestMethod]
        public void Invoice1_NetPayable() => Assert.AreEqual(this.Invoice().NetTotal, 29.83m);

        #endregion
    }
}
