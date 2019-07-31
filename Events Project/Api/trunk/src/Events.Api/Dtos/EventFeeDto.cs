using System;

namespace Aafp.Events.Api.Dtos
{
    public class EventFeeDto
    {
        public decimal Price { get; set; }

        public Guid PriceKey { get; set; }

        public Guid ProductKey { get; set; }

        public string ProductName { get; set; }
    }
}