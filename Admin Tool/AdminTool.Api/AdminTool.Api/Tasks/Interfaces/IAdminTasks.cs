using AdminTool.Api.Dto;

namespace AdminTool.Api.Tasks.Interfaces
{
    public interface IAdminTasks
    {
        UserRolesDto GetUserRoles(string userName);
    }
}
