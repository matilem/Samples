using AdminTool.Api.Dao.Queries.Interfaces;
using AdminTool.Api.Dto;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdminTool.Api.Dao.Queries
{
    public class HydraPermissionQuery : IHydraPermissionQuery
    {
        //public List<HydraUserPermissionDto> GetUserHydraPermissions(string userName)
        //{
        //    var dto = new List<HydraUserPermissionDto>();

        //    using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
        //    {
        //        connection.Open();
        //        dto = connection.Query<HydraUserPermissionDto>("get_hydra_user_permissions", new { userName }, commandType: CommandType.StoredProcedure).ToList();
        //    }

        //    return dto;
        //}
    }
}