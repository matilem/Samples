using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Dapper;
using System.Collections.Generic;

namespace Aafp.Cme.Api.Daos.Queries
{
    public class CmeActivityQuery : ICmeActivityQuery
    {
        public ICmeActivitySessionQuery CmeActivitySessionQuery { get; set; }

        public CmeActivityDto GetCmeSessionsByActivity(int activityNumber)
        {
            var dto = new CmeActivityDto();
            dto.Sessions = new List<CmeActivitySessionDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CmeActivityDto>("client_aafp_get_cme_credits_by_activity", new { activityNumber }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            if (dto != null)
                dto.Sessions = CmeActivitySessionQuery.GetCmeSessionsByActivity(activityNumber);

            return dto;
        }
    }
}