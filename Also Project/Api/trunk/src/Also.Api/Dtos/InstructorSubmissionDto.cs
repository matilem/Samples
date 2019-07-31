using System;

namespace Aafp.Also.Api.Dtos
{
    public class InstructorSubmissionDto
    {
        public Guid InstructorKey { get; set; }

        public Guid CustomerKey { get; set; }

        public bool AdvisoryFacultyRecommended { get; set; }

        public bool InstructorRecommended { get; set; }
    }
}