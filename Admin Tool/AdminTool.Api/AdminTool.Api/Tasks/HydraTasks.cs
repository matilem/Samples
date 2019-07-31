using AdminTool.Api.Dao.Commands;
using AdminTool.Api.Dao.Queries;
using AdminTool.Api.Dto;
using AdminTool.Api.Task.Interfaces;
using System.Collections.Generic;

namespace AdminTool.Api.Task
{
    public class HydraTasks 
    {
        //HydraPermission hydraPermission = new HydraPermission();

        //HydraPermissionQuery hydraPermissionQuery = new HydraPermissionQuery();

        //public List<HydraUserPermissionDto> GetUserPermissions(string userName)
        //{
        //    var userPermissions = new List<HydraUserPermissionDto>();

        //    userPermissions = hydraPermissionQuery.GetUserHydraPermissions(userName);

        //    return userPermissions;
        //}

        //public bool SaveUserPermissions(HydraUserSubmissionDto dto)
        //{
        //    var success = false;

        //    if (dto.Status == "New")
        //    {
        //        success = AddUserPermission(dto);
        //    }
        //    else
        //    {
        //        success = UpdateUserPermission(dto);
        //    }

        //    return success;
        //}

        //private bool AddUserPermission(HydraUserSubmissionDto dto)
        //{
        //    var success = false;

        //    success = hydraPermission.AddNewHydraUserPermission(dto);

        //    return success;
        //}

        //private bool UpdateUserPermission(HydraUserSubmissionDto dto)
        //{
        //    var success = false;

        //    success = hydraPermission.UpdateHydraUserPermission(dto);

        //    return success;
        //}
    }
}