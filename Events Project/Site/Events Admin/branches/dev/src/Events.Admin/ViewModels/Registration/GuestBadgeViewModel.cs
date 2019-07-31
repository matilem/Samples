using System;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class GuestBadgeViewModel
    {
        public Guid Key { get; set; }

        public Guid RegistrationKey { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
    }
}