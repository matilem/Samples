using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationHomeViewModel : ViewModelBase
    {
        public List<RegistrationViewModel> PendingRegistrations { get; set; }

        public List<RegistrationViewModel> CurrentRegistrations { get; set; }

        public List<RegistrationViewModel> UpcomingRegistrations { get; set; }
    }
}