using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Models
{
    public class PendingRegistration
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual Guid? ParentRegistrationKey { get; set; }

        public virtual Guid? CustomerAddressKey { get; set; }

        public virtual Guid? CustomerPhoneKey { get; set; }

        public virtual string EmergencyContactName { get; set; }

        public virtual string EmergencyContactPhone { get; set; }

        public virtual DateTime RegistrationDate { get; set; }

        public virtual Guid? PriceKey { get; set; }

        public virtual Guid CustomerKey { get; set; }

        public virtual Guid EventKey { get; set; }

        public virtual Guid? RegistrantKey { get; set; }

        public virtual bool IsProcessed { get; set; }

        public virtual IList<PendingRegistrationBadge> Badges { get; set; }

        public virtual IList<PendingRegistrationSession> Sessions { get; set; } 
    }
}