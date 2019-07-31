using System;
using System.Collections.Generic;
using System.Linq;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class EditRegistrationViewModel : ViewModelBase
    {
        public UserSearchViewModel UserSearch { get; set; }

        public Guid Key { get; set; }

        public string CurrentUser { get; set; }

        public Guid? CustomerAddressKey { get; set; }

        public Guid? CustomerPhoneKey { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactPhone { get; set; }

        public BadgeViewModel Badge { get; set; }

        public List<GuestBadgeViewModel> GuestBadges { get; set; }

        public CustomerViewModel Customer { get; set; }

        public EditEventDetailViewModel Event { get; set; }

        public int GuestCount { get; set; }

        public int SessionMaxTicketQuantity { get; set; }

    }
}