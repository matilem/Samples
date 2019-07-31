using System;
using System.Collections.Generic;

namespace Aafp.Events.Api.Models
{
    public class RegistrantSession
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual Registrant Registrant { get; set; }

        public virtual Session Session { get; set; }

        public virtual RegistrationInvoiceDetail InvoiceDetail { get; set; }

        public virtual DateTime? CancelDate { get; set; }

        public virtual int Quantity { get; set; }

        public virtual IList<RegistrationInvoiceDetail> InvoiceDetails { get; set; }
    }
}