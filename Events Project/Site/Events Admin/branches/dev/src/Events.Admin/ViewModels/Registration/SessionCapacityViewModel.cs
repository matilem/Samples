using System;

namespace Aafp.Events.Admin.ViewModels.Registration
{
    public class SessionCapacityViewModel : ViewModelBase
    {
        public Guid SessionKey { get; set; }

        public string SessionCode { get; set; }

        public string SessionTitle { get; set; }

        public int SessionCapacity { get; set; }

        public int SessionRegistrantCount { get; set; }

        public bool SessionTicketed { get; set; }

        public bool ShowCapacityWarning => SessionRegistrantCount == SessionCapacity;

        public bool AllowAdd => SessionRegistrantCount < SessionCapacity;
    }
}