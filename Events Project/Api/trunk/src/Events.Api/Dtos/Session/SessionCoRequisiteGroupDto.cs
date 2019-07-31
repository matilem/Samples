using System;
using System.Collections.Generic;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dtos.Session
{
    public class SessionCoRequisiteGroupDto
    {
        public Guid Key { get; set; }

        public Guid SessionKey { get; set; }

        public string Description { get; set; }

        public IList<SessionCoRequisite> RequiredSessions { get; set; }
    }
}