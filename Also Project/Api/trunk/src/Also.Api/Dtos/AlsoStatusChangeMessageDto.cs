using System;

namespace Aafp.Also.Api.Dtos
{
    public class AlsoStatusChangeMessageDto
    {
        public string AlsoStatus { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }

        public string Email { get; set; }
    }
}