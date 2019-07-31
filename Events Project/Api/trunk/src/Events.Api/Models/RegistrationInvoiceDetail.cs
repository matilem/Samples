using System;

namespace Aafp.Events.Api.Models
{
    public class RegistrationInvoiceDetail
    {
        public virtual Guid Key { get; set; }

        public virtual Guid? InvoiceKey { get; set; }

        public virtual int Parity { get; set; }

        public virtual Guid? PriceKey { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual decimal Price { get; set; }

        public virtual string PriceDisplayName { get; set; }

        public virtual string ProductName { get; set; }

        public virtual string InvoiceType { get; set; }

        public virtual string InvoiceCode { get; set; }

        public virtual string ProductCode { get; set; }

        public virtual DateTime? PriceEndDate { get; set; }

        public virtual bool InvoiceClosedFlag { get; set; }

        public virtual Guid? InvoiceAitKey { get; set; }
    }
}