using System;

namespace Aafp.Events.Api.Models
{
    public class SessionIcon
    {
        public virtual Guid Key { get; set; }

        public virtual string Description { get; set; }

        public virtual string URL { get; set; }
    }
}