using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public class AssessmentQuery : IAssessmentQuery
    {
        public Guid GetAssessmentGroupKeyByActivityNumber(int activityNumber)
        {
            var assessmentGroupKey = new Guid();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                assessmentGroupKey = connection.Query<Guid>("client_aafp_get_assessment_group_by_activity", new { activityNumber }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return assessmentGroupKey;
        }
    }
}