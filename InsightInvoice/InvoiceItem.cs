using System.Linq;

namespace InsightInvoice
{
    public class InvoiceItem
    {
        public Product Product { get; set; }
        public Packaging Packaging { get; set; }
        public decimal Quantity { get; set; }

        public decimal GrossPrice
        {
            get
            {
                var packqty = (this.Packaging?.PackQty ?? 1);
                var pricebase = (this.Product?.PricePer ?? 1);
                var productprice = this.Product?.Price ?? 0;

                var gross = (packqty * productprice) / pricebase;
                return gross.RoundDot05() * this.Quantity;
            }
        }

        public decimal Taxes
        {
            get
            {
                decimal taxTotal = 0;
                foreach(var tax in this.Product?.Category?.Taxes)
                {
                    taxTotal += (tax.Percentage * this.GrossPrice)/100;
                }
                return taxTotal.RoundDot05();
            }
        }

        public decimal NetPrice
        {
            get
            {
                return this.GrossPrice + this.Taxes; // Not clear should this be rounded to 2 decimal places.
            }
        }

        public string Verbose()
        {
            var taxRates = string.Join(",", this.Product?.Category?.Taxes?.Select(a => $"{a.Percentage}%"));
            return $"{this} [GP:{this.GrossPrice.ToString("n2")} Taxes({taxRates}):{this.Taxes.ToString("n2")}]";
        }

        public override string ToString()
        {
            var output = $"{this.Quantity}";

            if (this.Product?.NamePrefix != null)
            {
                output += $" {this.Product.NamePrefix.Trim()}";
            }
            if (this.Packaging?.Type != null)
            {
                output += $" {this.Packaging.Type.Trim()} of";
            }

            return $"{output} {this.Product?.Name}: {this.NetPrice.ToString("n2")}".ToLower();
        }
    }
}