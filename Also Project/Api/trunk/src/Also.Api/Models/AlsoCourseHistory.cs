using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Also.Api.Models
{
    public class AlsoCourseHistory
    {
        public virtual Guid Key { get; set; }

        public virtual string AddUser { get; set; }

        public virtual DateTime AddDate { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual DateTime? ChangeDate { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual Guid EntityKey { get; set; }

        public virtual Guid CustomerKey { get; set; }

        public virtual char Role { get; set; }

        public virtual Guid SessionKey { get; set; }
    }
}