using System;
using System.Collections.Generic;

namespace Aafp.Also.Web.ViewModels
{
    public class PostCourseSubmissionViewModel
    {
        public Guid ActivityKey { get; set; }

        public string ActivityNumber { get; set; }

        public string ActivityCourseType { get; set; }

        public DateTime ActivityEndDate { get; set; }

        public Guid AlsoCourseKey { get; set; }

        public List<LearnerSubmissionViewModel> Learners {get; set; }

        public List<InstructorSubmissionViewModel> Instructors { get; set; }

        public string WebLogin { get; set; }

        public string Status { get; set; }

        public string FileUploadLink { get; set; }
    }
}