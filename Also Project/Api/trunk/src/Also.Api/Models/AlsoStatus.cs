using System;

namespace Aafp.Also.Api.Models
{
    public class AlsoStatus
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual Guid EntityKey { get; set; }

        public virtual Guid CustomerKey { get; set; }

        public virtual Guid AlsoStatusTypeKey { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime ExpirationDate { get; set; }

        public virtual DateTime? ApproveDate { get; set; }
    }
}