using System;

namespace Aafp.Also.Api.Models
{
    public class Instructor
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual Guid EntityKey { get; set; }

        public virtual Guid AlsoCourseKey { get; set; }

        public virtual Guid CustomerKey { get; set; }
        
        public virtual Guid ActivityKey { get; set; }

        public virtual bool AdvisoryFacultyRecommended { get; set; }

        public virtual bool InstructorRecommended { get; set; }
    }
}