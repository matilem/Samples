using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Dapper;
using System;

namespace Aafp.Cme.Api.Daos.Queries
{
    public class CmeSessionQuery : ICmeSessionQuery
    {
        public CmeActivitySessionDto GetCmeSessionsByKey(Guid sessionKey)
        {
            var dto = new CmeActivitySessionDto();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CmeActivitySessionDto>("client_aafp_get_cme_credits_for_session_by_key", new { sessionKey }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return dto;
        }
    }
}