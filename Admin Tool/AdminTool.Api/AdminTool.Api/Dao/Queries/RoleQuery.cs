using AdminTool.Api.Dao.Queries.Interfaces;
using AdminTool.Api.Dto;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdminTool.Api.Dao.Queries
{
    public class RoleQuery : IRoleQuery
    {

        public List<RoleDto> GetRoles()
        {
            var dto = new List<RoleDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<RoleDto>("get_roles", null, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }

        public List<RoleDto> GetRolesForUser(string userName)
        {
            var dto = new List<RoleDto>();

            using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            {
                connection.Open();
                dto = connection.Query<RoleDto>("get_user_role_permission", new { userName }, commandType: CommandType.StoredProcedure).ToList();
            }

            return dto;
        }
    }
}