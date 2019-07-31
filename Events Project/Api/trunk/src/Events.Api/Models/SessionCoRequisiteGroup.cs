using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Models
{
    public class SessionCoRequisiteGroup
    {
        public virtual Guid Key { get; set; }

        public virtual Guid SessionKey { get; set; }

        public virtual string Description { get; set; }

        public virtual IList<SessionCoRequisite> RequiredSessions { get; set; }
    }
}