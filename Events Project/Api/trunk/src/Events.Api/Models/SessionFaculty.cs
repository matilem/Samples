using System;

namespace Aafp.Events.Api.Models
{
    public class SessionFaculty
    {
        private Guid key;
        private Guid customerKey;
        ////private Customer customer;

        public virtual Guid Key
        {
            get { return key; }
            set { key = value; }
        }

        public virtual Guid CustomerKey
        {
            get { return customerKey; }
            set { customerKey = value; }
        }

        ////public virtual Customer Customer
        ////{
        ////    get { return customer; }
        ////    set { customer = value; }
        ////}
    }
}