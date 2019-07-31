using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Events.Admin.ViewModels.Payment
{
    public class InvoiceDetailsViewModel
    {
        public Guid Key { get; set; }

        public Guid? InvoiceKey { get; set; }

        public Guid? PriceKey { get; set; }

        public decimal Price { get; set; }

        public string PriceDisplayName { get; set; }

        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public string InvoiceType { get; set; }

        public string InvoiceCode { get; set; }

        public string ProductCode { get; set; }

        public DateTime? PriceEndDate { get; set; }

        public string Time { get; set; }

        public bool InvoiceClosedFlag { get; set; }

        public string DiscountDisplay
        {
            get
            {
                var display = string.Empty;

                if (InvoiceType == "Discount")
                {
                    display = ProductCode != null ? $"{ProductCode} - {ProductName}" : $"{ProductName}";
                }
                return display;
            }
        }

        public string PriceDisplay => (Quantity * Price).ToString("F");

        public string PriceDateDisplay
        {
            get
            {
                var display = string.Empty;

                if (PriceEndDate.HasValue)
                {
                    display = $"{PriceEndDate.Value.ToString("ddd, MMM dd, yyyy")} </br> {Time}";
                }
                else
                {
                    display = "n/a";
                }

                return display;
            }
        }
    }
}