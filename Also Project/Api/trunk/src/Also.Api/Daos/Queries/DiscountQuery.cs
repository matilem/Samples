using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Also.Api.Daos.Queries
{
    public class DiscountQuery : IDiscountQuery
    {
        public Guid GetDiscount(string priceCode)
        {
            var prcKey = new Guid();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                prcKey = connection.Query<Guid>("get_also_discount", new { priceCode }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return prcKey;
        }
    }
}