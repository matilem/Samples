using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Also.Api.Daos.Queries
{
    public class ActivityPreCourseQuery : IActivityPreCourseQuery
    {
        public IMilitaryBranchesQuery MilitaryBranchesQuery { get; set; }

        public IActivityQuery ActivityQuery { get; set; }

        public ActivityPreCourseDto GetPreCourse(string activityNumber)
        {
            var dto = new ActivityPreCourseDto();
            dto.Activity = new ActivityDto();
            dto.MilitaryBranches = new List<MilitaryBranchDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<ActivityPreCourseDto>("get_also_pre_course", new { activityNumber }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            if (dto != null)
            {
                dto.Activity = ActivityQuery.GetActivity(activityNumber);
                dto.MilitaryBranches = MilitaryBranchesQuery.GetMilitaryBranches();
            }

            return dto;
        }
    }
}