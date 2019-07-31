using System.Collections.Generic;

namespace AdminTool.Api.Dto
{
    public class UserRolesDto
    {
        public string UserName { get; set; }

        public IList<RoleDto> UserRoles { get; set; }

        public IList<RoleDto> Roles { get; set; }
    }
}