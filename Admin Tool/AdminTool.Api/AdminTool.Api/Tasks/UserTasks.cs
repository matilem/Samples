using AdminTool.Api.Dto;
using AdminTool.Api.Task.Interfaces;
using System.DirectoryServices.AccountManagement;

namespace AdminTool.Api.Task
{
    public class UserTasks : IUserTasks
    {
        HydraTasks hydraTasks = new HydraTasks();

        public UserRolesDto ValidateHydraUser(string userName)
        {
            var dto = new UserRolesDto();

            // set up domain context
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, ApplicationConfig.ADLocation, ApplicationConfig.ADDC);
            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "Garfield.tpa.healtheval.com", "DC=TPA,DC=healtheval,DC=com");

            // find a user
            UserPrincipal user = UserPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, userName);

            // find the group in question
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, ApplicationConfig.HydraADGroup);

            if (user != null)
            {
                // check if user is member of that group
                if (user.IsMemberOf(group))
                {
                    dto.UserName = userName;
                    //hydraTasks.GetUserPermissions(userName);
                }
            }

            return dto;
        }

        public bool CreateActiveDirectoryUser(ActiveDirectorySubmissionDto dto)
        {
            var success = false;

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ApplicationConfig.ADLocation, ApplicationConfig.ADDC))
            {
                UserPrincipal existingUser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, dto.UserName);

                if (existingUser == null)
                {
                    //Add User
                    var samAccount = CreateUserAccount(pc, dto.UserName);

                    if (dto.HydraUser == true || dto.TookKitUser == true)
                    {
                        // Add user to group
                        GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, ApplicationConfig.HydraADGroup);
                        group.Members.Add(pc, IdentityType.UserPrincipalName, dto.UserName);
                        group.Save();
                    }
                }
            }

            return success;
        }

        public static string CreateUserAccount(PrincipalContext pc, string userName)
        {
            UserPrincipal up = new UserPrincipal(pc);
            up.SamAccountName = userName;
            up.SetPassword("Welcome1");
            up.Enabled = true;
            up.ExpirePasswordNow();
            up.Save();

            return up.SamAccountName;
        }
    }
}