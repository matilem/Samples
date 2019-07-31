using System;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class RegistrationSessionFeeDto
    {
        public decimal Price { get; set; }

        public Guid SessionKey { get; set; }

        public Guid PriceKey { get; set; }

        public Guid ProductKey { get; set; }
    }
}