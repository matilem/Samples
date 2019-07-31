using System;

namespace Aafp.Events.Api.Dtos.Admin.Registration
{
    public class AdminEditRegistrationGuestBadgeDto
    {
        public Guid Key { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string SessionCode { get; set; }

        public string SessionTitle { get; set; }
    }
}