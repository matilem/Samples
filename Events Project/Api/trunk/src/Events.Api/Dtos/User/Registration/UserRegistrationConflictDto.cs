using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationConflictDto
    {
        public Guid RegistrationKey { get; set; }

        public decimal CurrentTotal { get; set; }

        public UserEventDto Event { get; set; }

        public List<UserRegistrationSessionConflictGroupDto> ConflictGroups { get; set; }

        public List<EventStepDto> PendingSteps { get; set; }

        public Dictionary<Guid, UserEventDto> PendingEvents { get; set; }

        public string RegistrationStatus { get; set; }
    }
}