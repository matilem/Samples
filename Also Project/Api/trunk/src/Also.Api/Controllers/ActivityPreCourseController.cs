using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aafp.Also.Api.Controllers
{
    [RoutePrefix("precourse")]
    public class ActivityPreCourseController : ApiController
    {
        public IActivityPreCourseTasks PreCourseTasks { get; set; }

        public DiscountController DiscountController { get; set; }

        [Route("{activityNumber}/{webLogin}")]
        public async Task<IHttpActionResult> GetActivityPreCourse(string activityNumber, string webLogin)
        {
            var dto = await PreCourseTasks.GetPreCourse(activityNumber, webLogin);

            if (dto == null)
                return BadRequest("The provided activity number is not valid.");

            return Ok(dto);
        }

        [Route("save")]
        [HttpPost]
        public async Task<IHttpActionResult> SavePreCourse([FromBody] ActivityPreCourseSubmissionDto dto)
        {
            try
            {
                var result = await PreCourseTasks.SavePreCourse(dto);

                return Ok(result);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}