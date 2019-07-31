using AdminTool.Api.Dto;
using System.Collections.Generic;

namespace AdminTool.Api.Dao.Queries.Interfaces
{
    public interface IRoleQuery
    {
        List<RoleDto> GetRoles();

        List<RoleDto> GetRolesForUser(string userName);
    }
}
