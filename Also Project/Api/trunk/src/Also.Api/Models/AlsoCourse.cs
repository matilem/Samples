using System;

namespace Aafp.Also.Api.Models
{
    public class AlsoCourse
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual Guid EntityKey { get; set; }

        public virtual Guid ActivityKey { get; set; }

        public virtual Guid PriceKey { get; set; }

        public virtual Guid MilitaryKey { get; set; }

        public virtual bool PreCourseSubmittedFlag { get; set; }

        public virtual bool PreCourseApprovedFlag { get; set; }

        public virtual bool PostCourseSubmittedFlag { get; set; }

        public virtual bool PostCourseCompletedFlag { get; set; }
    }
}