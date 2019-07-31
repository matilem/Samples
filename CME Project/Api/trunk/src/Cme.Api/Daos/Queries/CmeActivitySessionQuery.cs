using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Dapper;
using System.Collections.Generic;

namespace Aafp.Cme.Api.Daos.Queries
{
    public class CmeActivitySessionQuery : ICmeActivitySessionQuery
    {
        public List<CmeActivitySessionDto> GetCmeSessionsByActivity(int activityNumber)
        {
            var dto = new List<CmeActivitySessionDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CmeActivitySessionDto>("client_aafp_get_cme_credits_for_session_by_activity", new { activityNumber }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }
    }
}