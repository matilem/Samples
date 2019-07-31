using System;

namespace Aafp.Also.Api.Dtos
{
    public class ActivityPreCourseSubmissionDto
    {
        public Guid ActivityKey { get; set; }

        public string ActivityNumber { get; set; }

        public string ActivityCourseType { get; set; }

        public string WebLogin { get; set; }

        public Guid CourseDirectorKey { get; set; }

        public string CourseDirectorId { get; set; }

        public string CourseDirectorName { get; set; }

        public string CourseDirectorEmail { get; set; }

        public string CourseDirectorPhone { get; set; }

        public Guid CourseCoordinatorKey { get; set; }

        public string CourseCoordinatorId { get; set; }

        public string CourseCoordinatorName { get; set; }

        public string CourseCoordinatorEmail { get; set; }

        public string CourseCoordinatorPhone { get; set; }

        public Guid AlsoCourseKey { get; set; }

        public Guid MilitaryBranchKey { get; set; }

        public string Status { get; set; }

        public DateTime ActivityBeginDate { get; set; }

        public DateTime ActivityEndDate { get; set; }

        public string ActivityLocation { get; set; }

        public string ActivitySponsorName { get; set; }
    }
}