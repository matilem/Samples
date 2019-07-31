using System;

namespace Aafp.Also.Web.ViewModels
{
    public class InstructorSubmissionViewModel
    {
        public Guid InstructorKey { get; set; }

        public Guid CustomerKey { get; set; }

        public bool AdvisoryFacultyRecommended { get; set; }

        public bool InstructorRecommended { get; set; }
    }
}