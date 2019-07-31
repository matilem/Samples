using AdminTool.Api.Dto;
using AdminTool.Api.Helpers;
using AdminTool.Api.Task;
using System;
using System.Web.Http;

namespace AdminTool.Api.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        UserTasks userTasks = new UserTasks();

        [Route("validate/{userName}/")]
        public IHttpActionResult GetActiveDirectoryUser(string userName)
        {
            var dto = userTasks.ValidateHydraUser(userName);

            if (dto == null)
                return BadRequest($"Unable to validate user: {userName}.");

            return Ok(dto);


        }

        [Route("create")]
        [HttpPost]
        public IHttpActionResult CreateActiveDirectoryUser([FromBody] ActiveDirectorySubmissionDto dto)
        {
            try
            {
                var result = userTasks.CreateActiveDirectoryUser(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"Unable to create user: {dto.UserName}.";
                Logger.LogError(message);
                return BadRequest(ex.Message);
            }
        }
    }
}
