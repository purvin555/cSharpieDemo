using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsightInvoice.Tests
{
    [TestClass]
    public class InvoiceTest3
    {

        private InvoiceTestSetup setup = new InvoiceTestSetup();

        #region Imported Perfume Item

        public InvoiceItem ImportedPerfumeScan()
            => setup.Scan("Imported Perfume", price: 27.99m, quantity: 1, category: setup.Import(), packaging: setup.BottlePackaging());

        [TestMethod]
        public void Invoice3_ImportedPerfumeTaxes() => Assert.AreEqual(this.ImportedPerfumeScan().Taxes, 4.2m);

        [TestMethod]
        public void Invoice3_ImportedPerfumePayable() => Assert.AreEqual(this.ImportedPerfumeScan().NetPrice, 32.19m);

        #endregion Imported Perfume Item

        #region Local Perfume Item

        public InvoiceItem PerfumeScan()
            => setup.Scan("Perfume", price: 18.99m, quantity: 1, category: setup.Local(), packaging: setup.BottlePackaging());

        [TestMethod]
        public void Invoice3_LocalPerfumeTaxes() => Assert.AreEqual(this.PerfumeScan().Taxes, 1.9m);

        [TestMethod]
        public void Invoice3_LocalPerfumePayable() => Assert.AreEqual(this.PerfumeScan().NetPrice, 20.89m);

        #endregion Local Perfume Item

        #region Pills Item

        public InvoiceItem PillsScan()
            => setup.Scan("Headache Pills", price: 9.75m, quantity: 1, category: setup.LocalSalesTaxExempt(), packaging: setup.PacketPackaging());

        [TestMethod]
        public void Invoice3_LocalPillsTaxes() => Assert.AreEqual(this.PillsScan().Taxes,0m);

        [TestMethod]
        public void Invoice3_LocalPillsPayable() => Assert.AreEqual(this.PillsScan().NetPrice, 9.75m);

        #endregion Local Perfume Item

        #region Imported Chocolate Item

        public InvoiceItem ImportedChocolateScan()
            => setup.Scan("Chocolate Bar", price: 11.25m, quantity: 1, category: setup.ImportSalesTaxExempt(), packaging: setup.BoxPackaging());

        [TestMethod]
        public void Invoice3_ImportedChocolateTaxes() => Assert.AreEqual(this.ImportedChocolateScan().Taxes, 0.6m);

        [TestMethod]
        public void Invoice3_ImportedChocolatePayable() => Assert.AreEqual(this.ImportedChocolateScan().NetPrice, 11.85m);

        #endregion Imported Chocolate Item

        #region Invoice Of All Items

        public Invoice Invoice()
            => new Invoice { Items = { this.ImportedPerfumeScan(), this.PerfumeScan(), this.PillsScan(), this.ImportedChocolateScan() } };

        [TestMethod]
        public void Invoice3_Taxes() => Assert.AreEqual(this.Invoice().Taxes, 6.70m);

        [TestMethod]
        public void Invoice3_NetPayable() => Assert.AreEqual(this.Invoice().NetTotal, 74.68m);

        #endregion
    }
}
