namespace Aafp.Events.Api.Models
{
    using System;

    public class SessionConflict
    {
        public virtual Guid Key { get; set; }

        public virtual Session ConflictSession { get; set; }

        public virtual int Type { get; set; }
    }
}