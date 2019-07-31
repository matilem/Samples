using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Models
{
    public class Registrant
    {
        public virtual Guid Key { get; set; }

        public virtual Guid EventKey { get; set; }

        public virtual Guid CustomerKey { get; set; }

        public virtual DateTime? CancelationDate { get; set; }

        public virtual RegistrationInvoiceDetail InvoiceDetail { get; set; }

        public virtual string BadgeName { get; set; }

        public virtual string Title { get; set; }

        public virtual string Country { get; set; }

        public virtual string State { get; set; }

        public virtual string City { get; set; }

        public virtual string Organization { get; set; }

        public virtual DateTime RegistrationDate { get; set; }

        public virtual string Comment { get; set; }

        public virtual string EmergencyContactName { get; set; }

        public virtual string EmergencyContactPhone { get; set; }

        public virtual Guid? AddressKey { get; set; }

        public virtual Guid? PhoneKey { get; set; }

        public virtual IList<RegistrantSession> Sessions { get; set; }

        public virtual IList<Guest> Guests { get; set; }

        public virtual IList<RegistrationInvoiceDetail> InvoiceDetails { get; set; }

        public virtual string InvoiceCode { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime ChangeDate { get; set; }
    }
}