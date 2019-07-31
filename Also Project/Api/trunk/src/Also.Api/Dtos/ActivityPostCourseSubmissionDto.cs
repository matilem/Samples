using System;
using System.Collections.Generic;

namespace Aafp.Also.Api.Dtos
{
    public class ActivityPostCourseSubmissionDto
    {
        public Guid ActivityKey { get; set; }

        public string ActivityNumber { get; set; }

        public string ActivityCourseType { get; set; }

        public Guid AlsoCourseKey { get; set; }

        public List<LearnerSubmissionDto> Learners { get; set; }

        public List<InstructorSubmissionDto> Instructors { get; set; }

        public string WebLogin { get; set; }

        public string Status { get; set; }

        public string FileUploadLink { get; set; }
    }
}