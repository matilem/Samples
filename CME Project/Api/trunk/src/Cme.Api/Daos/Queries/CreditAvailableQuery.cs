using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Aafp.Cme.Api.Dtos;
using Dapper;

namespace Aafp.Cme.Api.Daos.Queries
{
    public class CreditAvailableQuery : ICreditAvailableQuery
    {
        public List<CreditAvailableDto> GetPurchasedByCustomer(Guid customerKey)
        {
            var dto = new List<CreditAvailableDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CreditAvailableDto>("client_aafp_get_purchased_cme_by_customer", new { customerKey }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public List<CreditAvailableDto> GetSubscriptionsByCustomer(Guid customerKey)
        {
            var dto = new List<CreditAvailableDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<CreditAvailableDto>("client_aafp_get_subscription_cme_by_customer", new { customerKey }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }
    }
}