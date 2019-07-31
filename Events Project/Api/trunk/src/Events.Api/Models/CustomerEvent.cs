using System;

namespace Aafp.Events.Api.Models
{
    public class CustomerEvent
    {
        public virtual Guid EventKey { get; set; }

        public virtual string Title { get; set; }

        public virtual string Code { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual Guid RegistrationKey { get; set; }

        public virtual Guid CustomerKey { get; set; }

        public virtual bool IsPending { get; set; }
    }
}