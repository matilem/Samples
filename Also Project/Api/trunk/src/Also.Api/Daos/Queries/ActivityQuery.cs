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
    public class ActivityQuery : IActivityQuery
    {
        public List<ActivityDto> GetLearnerAlsoActivities(Guid customerKey)
        {
            var dto = new List<ActivityDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<ActivityDto>("get_also_learner_activities", new { customerKey }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public List<ActivityDto> GetAlsoActivitiesForStaff()
        {
            var dto = new List<ActivityDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<ActivityDto>("get_also_activities_for_staff", null, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public ActivityDto GetActivity(string activityNumber)
        {
            var dto = new ActivityDto();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<ActivityDto>("get_also_activity", new { activityNumber }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return dto;
        }
    }
}