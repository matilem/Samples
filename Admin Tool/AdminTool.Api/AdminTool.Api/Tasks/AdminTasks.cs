using AdminTool.Api.Dao.Commands;
using AdminTool.Api.Dao.Queries;
using AdminTool.Api.Dto;
using AdminTool.Api.Tasks.Interfaces;
using System.Collections.Generic;

namespace AdminTool.Api.Tasks
{
    public class AdminTasks : IAdminTasks
    {
        RoleQuery RoleQuery = new RoleQuery();
        UserRole UserRole = new UserRole();

        public UserRolesDto GetUserRoles(string userName)
        {
            var dto = new UserRolesDto();
            dto.Roles = new List<RoleDto>();
            dto.UserRoles = new List<RoleDto>();

            dto.UserName = userName;
            dto.Roles = RoleQuery.GetRoles();
            dto.UserRoles = RoleQuery.GetRolesForUser(userName);

            return dto;
        }

        public bool SaveUserPermissions(UserRoleSubmissionDto dto)
        {
            var success = false;

            if (dto.Status == "New")
            {
                success = AddUserPermission(dto);
            }
            else
            {
                success = UpdateUserPermission(dto);
            }

            return success;
        }

        private bool AddUserPermission(UserRoleSubmissionDto dto)
        {
            var success = false;

            success = UserRole.AddUserPermission(dto);

            return success;
        }

        private bool UpdateUserPermission(UserRoleSubmissionDto dto)
        {
            var success = false;

            //success = hydraPermission.UpdateUserPermission(dto);

            return success;
        }
    }
}