using System;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class EditedRegistrationGuestBadgeDto
    {
        public Guid Key { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string SessionCode { get; set; }

        public string SessionTitle { get; set; }
    }
}