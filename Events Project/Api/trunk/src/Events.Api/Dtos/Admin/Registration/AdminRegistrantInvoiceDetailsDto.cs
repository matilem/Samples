using System;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminRegistrantInvoiceDetailsDto
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

        public bool InvoiceClosedFlag { get; set; }
    }
}