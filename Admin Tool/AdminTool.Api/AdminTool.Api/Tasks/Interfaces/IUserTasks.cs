using AdminTool.Api.Dto;

namespace AdminTool.Api.Task.Interfaces
{
    public interface IUserTasks
    {
        UserRolesDto ValidateHydraUser(string userName);
    }
}
