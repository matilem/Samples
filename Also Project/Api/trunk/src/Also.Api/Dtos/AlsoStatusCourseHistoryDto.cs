using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aafp.Also.Api.Dtos
{
    public class AlsoStatusCourseHistoryDto
    {
        public Guid AlsoStatusKey { get; set; }

        public string AlsoStatusAddUser { get; set; }

        public DateTime AlsoStatusAddDate { get; set; }

        public char AlsoStatusRole { get; set; }

        public string ActivityCourseType { get; set; }
    }
}