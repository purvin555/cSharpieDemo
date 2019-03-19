using System;

namespace InsightInvoice
{
    public static class ExtensionMethods
    {
        public static decimal RoundDot05(this decimal item)
        {
            return (item * 100) % 1 == 0 ? item: Math.Ceiling(item * 20) / 20;
        }
    }
}