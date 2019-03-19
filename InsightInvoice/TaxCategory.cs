using System.Collections.Generic;

namespace InsightInvoice
{
    public class TaxCategory
    {
        public string Name { get;set;}
        public IList<Tax> Taxes { get; set; } = new List<Tax>();
    }
}