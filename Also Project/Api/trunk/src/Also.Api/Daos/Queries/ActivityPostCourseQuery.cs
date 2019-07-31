using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Also.Api.Daos.Queries
{
    public class ActivityPostCourseQuery : IActivityPostCourseQuery
    {
        public List<ActivityPostCourseLearnerDto> GetActivityLearners(string activityNumber)
        {
            var dto = new List<ActivityPostCourseLearnerDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<ActivityPostCourseLearnerDto>("get_also_learners", new { activityNumber }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public bool VerifyEligibility(Guid customerKey, string activityType)
        {
            var eligible = false;

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                eligible = connection.Query<bool>("get_also_activity_eligibility", new { customerKey, activityType }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return eligible;
        }

        public List<ActivityPostCourseInstructorDto> GetActivityInstructors(Guid activityKey)
        {
            var dto = new List<ActivityPostCourseInstructorDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<ActivityPostCourseInstructorDto>("get_also_instructors", new { activityKey }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }
    }
}
