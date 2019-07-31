using System;

namespace Aafp.Payments.Api.Models
{
    public class Discount
    {
        public virtual Guid PriceKey { get; set; }

        public virtual decimal DiscountPrice { get; set; }

        public virtual decimal DiscountPercent { get; set; }

        public virtual string DiscountCode { get; set; }

        public virtual string Name { get; set; }

        public virtual string Code { get; set; }
    }
}