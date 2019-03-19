using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsightInvoice.Tests
{
    [TestClass]
    public class InvoiceTest2
    {
        private InvoiceTestSetup setup = new InvoiceTestSetup();

        #region Imported Chocolate Item

        public InvoiceItem ChocolateScan()
            => setup.Scan("Chocolate Bar", price: 10m, quantity: 1, category: setup.ImportSalesTaxExempt(), packaging: setup.BoxPackaging());

        [TestMethod]
        public void Invoice2_ImportedChocolateTaxes() => Assert.AreEqual(this.ChocolateScan().Taxes, 0.5m);

        [TestMethod]
        public void Invoice2_ImportedChocolatePayable() => Assert.AreEqual(this.ChocolateScan().NetPrice, 10.5m);

        #endregion Imported Chocolate Ite

        #region Imported Perfume Item

        public InvoiceItem PerfumeScan()
            => setup.Scan("Perfume", price: 47.5m, quantity: 1, category: setup.Import(), packaging: setup.BottlePackaging());

        [TestMethod]
        public void Invoice2_ImportedPerfumeTaxes() => Assert.AreEqual(this.PerfumeScan().Taxes, 7.15m);

        [TestMethod]
        public void Invoice2_ImportedPerfumePayable() => Assert.AreEqual(this.PerfumeScan().NetPrice, 54.65m);

        #endregion Imported Perfume Item

        #region Invoice Of All Items

        public Invoice Invoice()
            => new Invoice { Items = { this.ChocolateScan(), this.PerfumeScan() } };

        [TestMethod]
        public void Invoice2_Taxes() => Assert.AreEqual(this.Invoice().Taxes, 7.65m);

        [TestMethod]
        public void Invoice2_NetPayable() => Assert.AreEqual(this.Invoice().NetTotal, 65.15m);

        #endregion
    }
}
