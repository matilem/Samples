using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Also.Api.Dtos
{
    public class AlsoCourseHistoryDto
    {
        public Guid AlsoCourseHistoryKey { get; set; }

        public string AddUser { get; set; }

        public DateTime AddDate { get; set; }

        public string ChangeUser { get; set; }

        public DateTime? ChangeDate { get; set; }

        public bool IsDeleted { get; set; }

        public Guid EntityKey { get; set; }

        public Guid CustomerKey { get; set; }

        public char Role { get; set; }

        public Guid SessionKey { get; set; }
    }
}