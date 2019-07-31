using System;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class RegistrantEmailViewModel : ViewModelBase
    {
        public Guid RegistrantKey { get; set; }

        public string FullName { get; set; }

        public string EventTitle { get; set; }

        public string EmailAddress { get; set; }
    }
}