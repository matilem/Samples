using System;

namespace Aafp.Also.Api.Models
{
    public class Activity
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual Guid EntityKey { get; set; }

        public virtual Guid DirectorKey { get; set; }

        public virtual string DirectorId { get; set; }

        public virtual string DirectorName { get; set; }

        public virtual string DirectorEmail { get; set; }

        public virtual string DirectorPhone { get; set; }

        public virtual Guid CoordinatorKey { get; set; }

        public virtual string CoordinatorId { get; set; }

        public virtual string CoordinatorName { get; set; }

        public virtual string CoordinatorEmail { get; set; }

        public virtual string CoordinatorPhone { get; set; }

        public virtual DateTime BeginDate { get; set; }

        public virtual DateTime EndDate { get; set; }
    }
}