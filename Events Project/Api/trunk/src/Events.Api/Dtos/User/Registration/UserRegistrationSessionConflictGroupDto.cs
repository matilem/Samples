using System.Collections.Generic;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationSessionConflictGroupDto
    {
        public bool DoNotAllow { get; set; }

        public List<UserRegistrationSessionDto> ConflictedSessions { get; set; }
    }
}