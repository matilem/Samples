using System.Collections.Generic;

namespace AdminTool.Api.Dto
{
    public class UserRoleSubmissionDto
    {
        public string UserName { get; set; }

        public IList<RoleDto> Roles { get; set; }

        public string Status { get; set; }
    }
}