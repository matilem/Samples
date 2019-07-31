using System;

namespace Aafp.Events.Api.Dtos.EditRegistration
{
    public class EditRegistrationGuestBadgeDto
    {
        public virtual Guid Key { get; set; }

        public virtual Guid RegistrationKey { get; set; }

        public virtual string Name { get; set; }

        public virtual string Location { get; set; }
    }
}