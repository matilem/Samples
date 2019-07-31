using Aafp.Also.Api.Dtos;
using System;
using System.Collections.Generic;

namespace Aafp.Also.Api.Daos.Queries.Interfaces
{
    public interface IActivityPostCourseQuery
    {
        List<ActivityPostCourseLearnerDto> GetActivityLearners(string activityNumber);

        bool VerifyEligibility(Guid cstKey, string activityType);

        List<ActivityPostCourseInstructorDto> GetActivityInstructors(Guid activityKey);
    }
}
