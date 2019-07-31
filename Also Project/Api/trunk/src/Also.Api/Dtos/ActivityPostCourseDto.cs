using System.Collections.Generic;

namespace Aafp.Also.Api.Dtos
{
    public class ActivityPostCourseDto
    {
        public List<ActivityPostCourseLearnerDto> Learners { get; set; }

        public List<ActivityPostCourseInstructorDto> Instructors { get; set; }

        public ActivityDto Activity { get; set; }

        public List<LearnerOccupationDto> LearnerOccupations { get; set; }

        public IndividualDto Customer { get; set; }

        public AlsoCourseDto AlsoCourse { get; set; }
    }
}