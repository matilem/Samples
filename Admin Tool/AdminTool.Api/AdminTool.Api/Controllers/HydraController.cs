using AdminTool.Api.Dto;
using AdminTool.Api.Helpers;
using AdminTool.Api.Task;
using System;
using System.Web.Http;

namespace AdminTool.Api.Controllers
{
    [RoutePrefix("hydra")]
    public class HydraController : ApiController
    {
        //    HydraTasks hydraTasks = new HydraTasks();

        //    [Route("permissions/{userName}/")]
        //    public IHttpActionResult GetUserPermissions(string userName)
        //    {
        //        HydraTasks hydraTasks = new HydraTasks();
        //        //var dto = hydraTasks.GetUserPermissions(userName);

        //        if (dto == null)
        //            return BadRequest($"Unable to load permissions for this user: {userName}.");

        //        return Ok(dto);
        //    }

        //    [Route("permissions/save")]
        //    [HttpPost]
        //    public IHttpActionResult AddNewHydraUser([FromBody] HydraUserSubmissionDto dto)
        //    {
        //        try
        //        {
        //            var result = hydraTasks.SaveUserPermissions(dto);

        //            return Ok(result);
        //        }
        //        catch (Exception ex)
        //        {
        //            var message = $"Unable to save user permissions: {dto.UserName}.";
        //            Logger.LogError(message);
        //            return BadRequest(ex.Message);
        //        }
        //    }
    }
}
