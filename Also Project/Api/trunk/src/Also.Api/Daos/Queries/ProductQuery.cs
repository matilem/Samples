using Aafp.Also.Api.Daos.Queries.Interfaces;
using Aafp.Also.Api.Dtos;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Aafp.Also.Api.Daos.Queries
{
    public class ProductQuery : IProductQuery
    {
        public ProductDto GetProduct(string productCode)
        {
            var dto = new ProductDto();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<ProductDto>("get_also_product", new { productCode }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return dto;
        }
    }
}