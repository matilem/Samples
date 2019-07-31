using System;

namespace Aafp.Events.Api.Models
{
    public class Fee
    {
        public virtual decimal Price { get; set; }

        public virtual Guid SessionKey { get; set; }

        public virtual Guid PriceKey { get; set; }

        public virtual Guid ProductKey { get; set; }

        public virtual string ProductName { get; set; }

        public virtual bool SellOnline { get; set; }
    }
}