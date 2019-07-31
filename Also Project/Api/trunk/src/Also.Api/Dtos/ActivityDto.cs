using System;

namespace Aafp.Also.Api.Dtos
{
    public class ActivityDto
    {
        public Guid ActivityKey { get; set; }

        public string ActivityNumber { get; set; }

        public string ActivityTitle { get; set; }

        public DateTime ActivityBeginDate { get; set; }

        public DateTime ActivityEndDate { get; set; }

        public string ActivityCourseType { get; set; }

        public string ActivityCity { get; set; }

        public string ActivityState { get; set; }

        public string ActivitySponsorName { get; set; }

        public bool ActivityPreCourseSubmitted { get; set; }

        public bool ActivityPreCourseApproved { get; set; }

        public bool ActivityPostCourseSubmitted { get; set; }

        public bool ActivityPostCourseCompleted { get; set; }

        public string ActivityDirectorName { get; set; }

        public string ActivityCoordinatorName { get; set; }

        public Guid ActivitySessionKey { get; set; }

        public string CMEApplicationStatus { get; set; }
    }
}