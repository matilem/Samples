using System;
using System.Collections.Generic;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class CustomerSearchViewModel : ViewModelBase
    {
        public UserSearchViewModel UserSearch { get; set; }

        public List<CustomerSearchResultViewModel> Results { get; set; } 

        public List<EventListItemViewModel> Events { get; set; } 

        public Guid EventToAddKey { set; get; }

        public string Status { get; set; }
    }
}