using System;

namespace Aafp.Events.Api.Dtos
{
    public class SessionFeeDto
    {
        public decimal Price { get; set; }

        public Guid SessionKey { get; set; }

        public Guid? PriceKey { get; set; }

        public Guid ProductKey { get; set; }
    }
}