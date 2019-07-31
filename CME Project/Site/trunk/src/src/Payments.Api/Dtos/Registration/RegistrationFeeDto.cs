using System;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class RegistrationFeeDto
    {
        public decimal Price { get; set; }

        public Guid PriceKey { get; set; }

        public Guid ProductKey { get; set; }

        public string ProductName { get; set; }
    }
}