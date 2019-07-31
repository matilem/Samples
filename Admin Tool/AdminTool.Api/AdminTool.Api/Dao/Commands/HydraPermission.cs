using AdminTool.Api.Dao.Commands.Interfaces;
using AdminTool.Api.Dao.Queries;
using AdminTool.Api.Dto;
using AdminTool.Api.Models;
using DapperExtensions;
using System.Data.SqlClient;

namespace AdminTool.Api.Dao.Commands
{
    public class HydraPermission : IHydraPermission
    {
        //HydraPermissionQuery hydraPermissionQuery = new HydraPermissionQuery();

        //public bool AddNewHydraUserPermission(HydraUserSubmissionDto dto)
        //{
        //    var success = false;

        //    foreach (var userPermission in dto.Permissions)
        //    {
        //        using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
        //        {
        //            connection.Open();
        //            Permission permission = new Permission
        //            {
        //                AccessName = dto.UserName,
        //                AccessType = "User",
        //                ResourceId = userPermission.ResourceId,
        //                RightPermission = true
        //            };

        //            int id = connection.Insert(permission);

        //            if (id != 0)
        //            {
        //                success = true;
        //            }
        //        }
        //    }

        //    return success;
        //}

        //public bool UpdateHydraUserPermission(HydraUserSubmissionDto dto)
        //{
        //    var success = false;
        //    var existingPermissions = hydraPermissionQuery.GetUserHydraPermissions(dto.UserName);

        //    foreach (var existingPermission in existingPermissions)
        //    {
        //        using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
        //        {
        //            connection.Open();
        //            Permission permission = connection.Get<Permission>(1);
        //            //connection.Delete(permission);
        //            permission.RightPermission = false;
        //            connection.Update(permission);
        //        }
        //    }

        //    foreach (var userPermission in dto.Permissions)
        //    {
        //        using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
        //        {
        //            connection.Open();
        //            Permission permission = new Permission
        //            {
        //                AccessName = dto.UserName,
        //                AccessType = "User",
        //                ResourceId = userPermission.ResourceId,
        //                RightPermission = true
        //            };

        //            int id = connection.Insert(permission);

        //            if (id != 0)
        //            {
        //                success = true;
        //            }
        //        }
        //    }

        //    return success;
        //}
    }
}