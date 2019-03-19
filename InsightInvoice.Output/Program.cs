using System;
using System.Collections.Generic;

namespace InsightInvoice.Output
{
    public class Program
    {
        public static void Main(string[] args)
        {
            do
            {
                Program.Output();
                Console.WriteLine("Press Esc to end program or press any key to change output format");
            }
            while (!(Console.ReadKey().Key == ConsoleKey.Escape));
        }

        private static void Output()
        {
            Console.Clear();
            Program program = new Program();
            Console.WriteLine("Press V for verbose output of tests otherwise press enter");
            var response = Console.ReadKey();
            var isVerbose = response.Key == ConsoleKey.V;
            Console.Clear();
            Console.WriteLine("---Test 1---");
            Console.WriteLine();
            var invoice1 = program.Test1();
            Console.WriteLine((isVerbose ? invoice1?.Verbose() : invoice1.ToString()));
            Console.WriteLine();

            Console.WriteLine("---Test 2---");
            Console.WriteLine();
            var invoice2 = program.Test2();
            Console.WriteLine((isVerbose ? invoice2?.Verbose() : invoice2.ToString()));
            Console.WriteLine();

            Console.WriteLine("---Test 3---");
            Console.WriteLine();
            var invoice3 = program.Test3();
            Console.WriteLine((isVerbose ? invoice3?.Verbose() : invoice3.ToString()));
            Console.WriteLine();

        }

        #region Tests

        #region state
        private Tax salesTax;
        private Tax importDuty;
        private TaxCategory localCategory;
        private TaxCategory importCategory ;
        private TaxCategory stExemptLocalCategory;
        private TaxCategory stExemptImportCategory;
        private Packaging boxPackaging;
        private Packaging bottlePackaging;
        private Packaging packetPackaging;
        #endregion

        private Program()
        {
            this.salesTax = new Tax { Name = "Sales Tax", Percentage = 10 };
            this.importDuty = new Tax { Name = "Import Duty", Percentage = 5 };
            this.boxPackaging = new Packaging { Type = "Box", PackQty = 1 };
            this.bottlePackaging = new Packaging { Type = "Bottle", PackQty = 1 };
            this.packetPackaging = new Packaging { Type = "Packet", PackQty = 1 };
            this.localCategory = new TaxCategory()
            {
                Name = "Local",
                Taxes = new List<Tax> { salesTax }
            };
            this.importCategory = new TaxCategory()
            {
                Name = "Imported",
                Taxes = new List<Tax> { salesTax, importDuty }
            };
            this.stExemptLocalCategory = new TaxCategory()
            {
                Name = "ST Exempt",
            };
            this.stExemptImportCategory = new TaxCategory()
            {
                Name = "ST Exempt",
                Taxes = new List<Tax> { importDuty }
            };
        }

        private Invoice Test1()
        {
            var book = new Product { Name = "Book", Category = stExemptLocalCategory, Price = 12.49m };
            var cd = new Product { Name = "Music CD", Category = localCategory, Price = 14.99m };
            var chocoBar = new Product { Name = "Chocolate Bar", Category = stExemptLocalCategory, Price = 0.85m };
            var invoice = new Invoice
            {
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = book, Quantity =1 } ,
                    new InvoiceItem { Product = cd, Quantity =1 } ,
                    new InvoiceItem { Product = chocoBar, Quantity =1 }
                }
            };
            return invoice;
        }

        private Invoice Test2()
        {
            var importedChocolate = new Product { Name = "Chocolates", Price = 10m, NamePrefix = "Imported", Category = stExemptImportCategory };
            var importedPerfume = new Product { Name = "Perfume", Price = 47.5m, NamePrefix = "Imported", Category = importCategory };
            var invoice = new Invoice
            {
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = importedChocolate, Quantity =1 ,Packaging = boxPackaging} ,
                    new InvoiceItem { Product = importedPerfume, Quantity =1,Packaging = bottlePackaging, } ,
                }
            };
            return invoice;
        }

        private Invoice Test3()
        {
            var importedPerfume = new Product { Name = "Perfume", Price = 27.99m, NamePrefix = "Imported",  Category = importCategory };
            var perfume = new Product { Name = "Perfume", Price = 18.99m, Category = localCategory };
            var pills = new Product { Name = "Headache Pills", Price = 9.75m, Category = stExemptLocalCategory };
            var importedChocolates = new Product { Name = "Chocolates", Price = 11.25m, NamePrefix = "Imported",  Category = stExemptImportCategory };
            var invoice = new Invoice
            {
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = importedPerfume, Quantity =1,Packaging = bottlePackaging } ,
                    new InvoiceItem { Product = perfume, Quantity =1,Packaging = bottlePackaging } ,
                    new InvoiceItem { Product = pills, Quantity =1 ,Packaging = packetPackaging} ,
                    new InvoiceItem { Product = importedChocolates, Quantity =1 ,Packaging = boxPackaging} ,
                }
            };
            return invoice;
        }

        #endregion
    }
}
