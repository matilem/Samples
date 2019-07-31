using System;

namespace Aafp.Events.Api.Models
{
    public class Guest
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual Guid RegistrationKey { get; set; }

        public virtual string Name { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual Guid? StateKey { get; set; }

        public virtual string Country { get; set; }

        public virtual Guid? CountryKey { get; set; }

        public virtual string Location { get; set; }
    }
}