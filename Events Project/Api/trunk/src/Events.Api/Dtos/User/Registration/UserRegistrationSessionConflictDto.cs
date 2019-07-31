using System;

namespace Aafp.Events.Api.Dtos.User.Registration
{
    public class UserRegistrationSessionConflictDto
    {
        public Guid Key { get; set; }

        public Guid ConflictSessionKey { get; set; }

        public string ConflictSessionCode { get; set; }

        public int Type { get; set; }
    }
}