using System;

namespace Aafp.Events.Api.Models
{
    public class SessionLocation
    {
        public virtual Guid Key { get; set; }

        public virtual string Location { get; set; }
    }
}