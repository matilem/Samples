using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aafp.Also.Api.Controllers
{
    [RoutePrefix("postcourse")]
    public class ActivityPostCourseController : ApiController
    {
        public IActivityPostCourseTasks PostCourseTasks { get; set; }

        [Route("{activityNumber}/{webLogin}")]

        public async Task<IHttpActionResult> GetActivityPostCourse(string activityNumber, string webLogin)
        {
            var dto = await PostCourseTasks.GetPostCourse(activityNumber, webLogin);

            if (dto == null)
                return BadRequest("The provided activity number is not valid.");

            return Ok(dto);
        }


        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SavePostCourse([FromBody] ActivityPostCourseSubmissionDto dto)
        {
            try
            {
                var result = await PostCourseTasks.SavePostCourse(dto);

                return Ok(result);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
