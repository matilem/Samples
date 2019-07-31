using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Dapper;

namespace Aafp.Cme.Api.Daos.Queries
{
    public class CreditTypeQuery : ICreditTypeQuery
    {
        public List<CreditTypeDto> GetAll()
        {
            var dto = new List<CreditTypeDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CreditTypeDto>("client_aafp_get_cme_credit_types", null, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public List<CreditTypeDto> GetByLimitType(string limitType)
        {
            var dto = new List<CreditTypeDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CreditTypeDto>("client_aafp_get_cme_credit_types_by_limit", new { limitType }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }
    }
}