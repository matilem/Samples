using System;

namespace Aafp.Events.Api.Dtos.Session
{
    public class SessionConflictDto
    {
        public Guid Key { get; set; }

        public Guid ConflictSessionKey { get; set; }

        public string ConflictSessionCode { get; set; }

        public int Type { get; set; }
    }
}