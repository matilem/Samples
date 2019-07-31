using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Aafp.Cme.Api.Daos.Queries.Interfaces;
using Dapper;

namespace Aafp.Cme.Api.Daos.Queries
{
    public class InvoiceQuery : IInvoiceQuery
    {
        public bool CheckUrlAccess(string webLogin, string url)
        {
            var hasAccess = false;

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                hasAccess = connection.Query<bool>("client_aafp_check_url_access", new { accessUrl = url, webLogin }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

            return hasAccess;
        }
    }
}