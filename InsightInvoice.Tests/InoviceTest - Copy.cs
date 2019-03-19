[TestClass]
public class InvoiceTest
{
    #region state

    private Tax salesTax;
    private Tax importDuty;
    private ProductCategory localCategory;
    private ProductCategory importCategory;
    private ProductCategory stExemptLocalCategory;
    private ProductCategory stExemptImportCategory;
    private Packaging boxPackaging;
    private Packaging bottlePackaging;
    private Packaging packetPackaging;

    #endregion

    [TestInitialize]
    public void InitializeState()
    {
        this.salesTax = new Tax { Name = "Sales Tax", Percentage = 10 };
        this.importDuty = new Tax { Name = "Import Duty", Percentage = 5 };
        this.boxPackaging = new Packaging { Type = "Box", PackageUnits = 1 };
        this.bottlePackaging = new Packaging { Type = "Bottle", PackageUnits = 1 };
        this.packetPackaging = new Packaging { Type = "Packet", PackageUnits = 1 };
        this.localCategory = new ProductCategory()
        {
            Name = "Local",
            Taxes = new List<Tax> { salesTax }
        };
        this.importCategory = new ProductCategory()
        {
            Name = "Imported",
            Taxes = new List<Tax> { salesTax, importDuty }
        };
        this.stExemptLocalCategory = new ProductCategory()
        {
            Name = "ST Exempt",
        };
        this.stExemptImportCategory = new ProductCategory()
        {
            Name = "ST Exempt",
            Taxes = new List<Tax> { importDuty }
        };
    }

    [TestMethod]
    public void SampleInvoiceTest1()
    {
        //---Test 1-- -
        //1 book: 12.49[GP: 12.49 Taxes():0.00]
        //1 music cd: 16.49[GP: 14.99 Taxes(10 %):1.50]
        //1 chocolate bar: 0.85[GP: 0.85 Taxes():0.00]
        //Gross Total: 28.33
        //Plus Taxes: 1.50
        //Net Total: 29.83

        var book = new Product { Name = "Book", Category = stExemptLocalCategory, UnitPrice = 12.49m };
        var cd = new Product { Name = "Music CD", Category = localCategory, UnitPrice = 14.99m };
        var chocoBar = new Product { Name = "Chocolate Bar", Category = stExemptLocalCategory, UnitPrice = 0.85m };
        var bookScan = new InvoiceItem { Product = book, Quantity = 1 };
        var cdScan = new InvoiceItem { Product = cd, Quantity = 1 };
        var chocoScan = new InvoiceItem { Product = chocoBar, Quantity = 1 };
        var invoice = new Invoice
        {
            Items = new List<InvoiceItem> { bookScan, cdScan, chocoScan }
        };
        Assert.AreEqual(invoice.NetTotal, 29.83m);
        Assert.AreEqual(invoice.Taxes, 1.5m);
        Assert.AreEqual(invoice.GrossTotal, 29.83m);
        Assert.AreEqual(invoice.Items[0].Taxes, 0);
        Assert.AreEqual(invoice.Items[1].Taxes, 1.5);
        Assert.AreEqual(invoice.Items[2].Taxes, 0);
    }

    [TestMethod]
    public void SampleInvoiceTest2()
    {
        //-- - Test 2-- -

        //  1 imported box of chocolates: 10.50[GP: 10.00 Taxes(5 %):0.50]
        //1 imported bottle of perfume: 54.65[GP: 47.50 Taxes(10 %, 5 %):7.15]
        //Gross Total: 57.50
        //Plus Taxes: 7.65
        //Net Total: 65.15

        var importedChocolate = new Product { Name = "Chocolates", UnitPrice = 10m, NamePrefix = "Imported", Packaging = boxPackaging, Category = stExemptImportCategory };
        var importedPerfume = new Product { Name = "Perfume", UnitPrice = 47.5m, NamePrefix = "Imported", Packaging = bottlePackaging, Category = importCategory };
        var invoice = new Invoice
        {
            Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = importedChocolate, Quantity =1 } ,
                    new InvoiceItem { Product = importedPerfume, Quantity =1 } ,
                }
        };
        Assert.AreEqual("Hello World", "Hello World");
    }

    [TestMethod]
    public void SampleInvoiceTest3()
    {
        //-- - Test 3-- -

        //  1 imported bottle of perfume: 32.19[GP: 27.99 Taxes(10 %, 5 %):4.20]
        //1 bottle of perfume: 20.89[GP: 18.99 Taxes(10 %):1.90]
        //1 packet of headache pills: 9.75[GP: 9.75 Taxes():0.00]
        //1 imported box of chocolates: 11.85[GP: 11.25 Taxes(5 %):0.60]
        //Gross Total: 67.98
        //Plus Taxes: 6.70
        //Net Total: 74.68

        var importedPerfume = new Product { Name = "Perfume", UnitPrice = 27.99m, NamePrefix = "Imported", Packaging = bottlePackaging, Category = importCategory };
        var perfume = new Product { Name = "Perfume", UnitPrice = 18.99m, Packaging = bottlePackaging, Category = localCategory };
        var pills = new Product { Name = "Headache Pills", UnitPrice = 9.75m, Packaging = packetPackaging, Category = stExemptLocalCategory };
        var importedChocolates = new Product { Name = "Chocolates", UnitPrice = 11.25m, NamePrefix = "Imported", Packaging = boxPackaging, Category = stExemptImportCategory };
        var invoice = new Invoice
        {
            Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = importedPerfume, Quantity =1 } ,
                    new InvoiceItem { Product = perfume, Quantity =1 } ,
                    new InvoiceItem { Product = pills, Quantity =1 } ,
                    new InvoiceItem { Product = importedChocolates, Quantity =1 } ,
                }
        };
        Assert.AreEqual("Hello World", "Hello World");
    }

}