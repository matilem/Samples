using System;

namespace Aafp.Events.Api.Models
{
    public class EditedRegistrationBadge
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual string NickName { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string State { get; set; }

        public virtual string City { get; set; }

        public virtual string Country { get; set; }

        public virtual string Notes { get; set; }

        public virtual string Company { get; set; }

        public virtual string Position { get; set; }

        public virtual bool IsGuest { get; set; }

        public virtual string GuestName { get; set; }

        public virtual string GuestLocation { get; set; }
    }
}