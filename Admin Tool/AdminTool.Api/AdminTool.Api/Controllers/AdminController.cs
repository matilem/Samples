using AdminTool.Api.Dto;
using AdminTool.Api.Helpers;
using AdminTool.Api.Tasks;
using System;
using System.Web.Http;

namespace AdminTool.Api.Controllers
{
    [RoutePrefix("admin")]
    public class AdminController : ApiController
    {
        AdminTasks AdminTasks = new AdminTasks();

        [Route("get/roles/")]
        public IHttpActionResult GetRoles(string userName)
        {
            var dto = AdminTasks.GetUserRoles(userName);

            if (dto == null)
                return BadRequest($"Unable to retrieve roles user: {userName}.");

            return Ok(dto);
        }

        [Route("permissions/save")]
        [HttpPost]
        public IHttpActionResult SaveUserRoles([FromBody] UserRoleSubmissionDto dto)
        {
            try
            {
                var result = AdminTasks.SaveUserPermissions(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"Unable to save user permissions: {dto.UserName}.";
                Logger.LogError(message);
                return BadRequest(ex.Message);
            }
        }

    }
}
