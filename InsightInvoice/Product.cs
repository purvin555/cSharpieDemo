namespace InsightInvoice
{
    public class Product
    {
        public string Name { get; set; }       
        public string NamePrefix { get; set; }
        public string NameSuffix { get; set; }
        public decimal Price { get; set; }
        public decimal PricePer { get; set; } = 1;
        public TaxCategory Category { get; set; }        
    }
}