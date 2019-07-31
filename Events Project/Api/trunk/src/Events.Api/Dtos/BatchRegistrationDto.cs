using System;

namespace Aafp.Events.Api.Dtos
{
    public class BatchRegistrationDto
    {
        public Guid CustomerKey { get; set; }

        public string MemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string RegistrationStatus { get; set; }

        public string PaymentType { get; set; }
    }
}