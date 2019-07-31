using Aafp.Also.Api.Tasks.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aafp.Also.Api.Controllers
{
    [RoutePrefix("activity")]
    public class ActivityController : ApiController
    {
        public IActivityTasks ActivityTasks { get; set; }

        [Route("{webLogin}")]
        public async Task<IHttpActionResult> GetActivitiesForCourseList(string webLogin)
        {
            var dto = await ActivityTasks.GetActivitiesForCourseList(webLogin);

            if (dto == null)
                return BadRequest("There were no activities for this user.");

            return Ok(dto);
        }
    }
}