using System;

namespace Aafp.Events.Api.Dtos.PendingRegistration
{
    public class PendingRegistrationSessionDto
    {
        public Guid Key { get; set; }

        public string Code { get; set; }

        public bool Ticketed { get; set; }

        public bool Selected { get; set; }

        public bool Quantity { get; set; }
    }
}