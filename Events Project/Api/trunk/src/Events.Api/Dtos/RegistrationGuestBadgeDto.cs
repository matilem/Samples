using System;

namespace Aafp.Events.Api.Dtos
{
    public class RegistrationGuestBadgeDto
    {
        public Guid Key { get; set; }

        public Guid RegistrationKey { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
    }
}