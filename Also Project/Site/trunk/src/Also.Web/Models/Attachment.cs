using System;

namespace Aafp.Also.Web.Models
{

    public class Attachment
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime ChangeDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual Guid EntityKey { get; set; }

        public virtual string FileType { get; set; }
        
        public virtual string FileLocation { get; set; }

        public virtual Guid CourseKey { get; set; }

    }
}