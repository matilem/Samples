using System;

namespace Aafp.Payments.Api.Dtos.Registration
{
    public class CustomerDto
    {
        public Guid Key { get; set; }

        public string CustomerId { get; set; }

        public string WebLogin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}