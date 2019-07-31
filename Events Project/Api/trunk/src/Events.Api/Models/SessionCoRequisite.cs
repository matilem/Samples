using System;

namespace Aafp.Events.Api.Models
{
    public class SessionCoRequisite
    {
        public virtual Guid Key { get; set; }

        public virtual Guid SessionKey { get; set; }

        public virtual Session Sessions { get; set; }
    }
}