using System;
using System.Collections.Generic;

namespace Aafp.Events.Web.ViewModels
{
    public class RegistrationConflictViewModel : ViewModelBase
    {
        public EventViewModel Event { get; set; }

        public bool AllowedConflicts { get; set; }

        public List<RegistrationSessionConflictGroupViewModel> ConflictGroups { get; set; }

        public List<EventStepViewModel> PendingSteps { get; set; }

        public Dictionary<Guid, EventViewModel> PendingEvents { get; set; }

        public string RegistrationStatus { get; set; }
    }
}