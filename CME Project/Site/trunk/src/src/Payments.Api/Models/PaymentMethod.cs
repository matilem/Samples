using System;

namespace Aafp.Payments.Api.Models
{
    public class PaymentMethod
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool DeleteFlag { get; set; }

        public virtual Guid? EntityKey { get; set; }

        public virtual string Method { get; set; }

        public virtual string Type { get; set; }
    }
}