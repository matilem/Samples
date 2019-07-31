using AdminTool.Api.Dto;
using AdminTool.Api.Models;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AdminTool.Api.Dao.Commands
{
    public class UserRole
    {

        //HydraPermissionQuery hydraPermissionQuery = new HydraPermissionQuery();

        public bool AddUserPermission(UserRoleSubmissionDto dto)
        {
            var success = false;

            foreach (var role in dto.Roles)
            {
                using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
                {
                    connection.Open();
                    RolePermission permission = new RolePermission
                    {
                        RoleId = role.RoleID,
                        sAMAccountName = dto.UserName,
                        ActiveFlag = true,
                        AddDate = DateTime.Now,
                        AddUser = "Logged in user"
                    };

                    int id = connection.Insert(permission);

                    if (id != 0)
                    {
                        success = true;
                    }
                }
            }

            return success;
        }

        //public bool UpdateUserPermission(UserRoleSubmissionDto dto)
        //{
            //var success = false;
            //var existingPermissions = hydraPermissionQuery.GetUserHydraPermissions(dto.UserName);

            //foreach (var existingPermission in existingPermissions)
            //{
            //    using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            //    {
            //        connection.Open();
            //        Permission permission = connection.Get<Permission>(1);
            //        //connection.Delete(permission);
            //        permission.RightPermission = false;
            //        connection.Update(permission);
            //    }
            //}

            //foreach (var userPermission in dto.Permissions)
            //{
            //    using (var connection = new SqlConnection(ApplicationConfig.DatabaseConnectionString))
            //    {
            //        connection.Open();
            //        Permission permission = new Permission
            //        {
            //            AccessName = dto.UserName,
            //            AccessType = "User",
            //            ResourceId = userPermission.ResourceId,
            //            RightPermission = true
            //        };

            //        int id = connection.Insert(permission);

            //        if (id != 0)
            //        {
            //            success = true;
            //        }
            //    }
            //}

            //return success;
        //}
    }
}