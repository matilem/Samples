using System;
using System.Collections.Generic;
using System.Linq;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class BatchViewModel : ViewModelBase
    {
        public UserSearchViewModel UserSearch { get; set; }

        public string CurrentUser { get; set; }

        public Guid RegistrationEventKey { get; set; }

        public List<EventListItemViewModel> Events { get; set; }

        public List<EventFeeViewModel> Fees { get; set; }

        public List<BatchCustomerViewModel> Registrants { get; set; }

        public Guid EventToAddKey { get; set; }

        public string FileName { get; set; }

        public string SuccessDisplay
        {
            get
            {
                var display = string.Empty;

                if (Registrants == null)
                {
                    return display;
                }

                var count = Registrants.Count();

                int i = Registrants.Count(registrant => registrant.RegistrationStatus == "Successful");

                display = i + "/" + count;

                return display;
            }
        }
    }
}