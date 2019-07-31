using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Customer;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationIntroDto
    {
        public Guid RegistrationKey { get; set; }

        public Guid? SelectedPriceKey { get; set; }

        public bool UserIsOnWaitList { get; set; }

        public bool IsPending { get; set; }

        public bool IsRegistered { get; set; }

        public decimal CurrentTotal { get; set; }

        public decimal CurrentSessionsCost { get; set; }

        public CustomerDto Customer { get; set; }

        public UserEventDto Event { get; set; }
        
        public List<UserRegistrationIntroDto> RelatedRegistrations { get; set; }

        public List<EventStepDto> PendingSteps { get; set; }

        public string Status { get; set; }
    }
}