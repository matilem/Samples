using System;
using System.Collections.Generic;

namespace Aafp.Also.Api.Dtos
{
    public class ActivityPreCourseDto
    {
        public Guid ApplicationKey { get; set; }

        public ActivityDto Activity { get; set; }

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

        public string ActivitySessionAgendaUrl { get; set; }

        public string ActivityCity { get; set; }

        public string ActivityState { get; set; }

        public string ActivitySponsorName { get; set; }

        public List<MilitaryBranchDto> MilitaryBranches { get; set; }

        public IndividualDto Customer { get; set; }

        public AlsoCourseDto AlsoCourse { get; set; }
    }
}