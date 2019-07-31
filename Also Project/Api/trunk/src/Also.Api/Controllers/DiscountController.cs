using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using System.Web.Http;

namespace Aafp.Also.Api.Controllers
{
    [RoutePrefix("discount")]
    public class DiscountController : ApiController
    {
        public IDiscountTasks DiscountTasks { get; set; }

        [Route("create")]
        [HttpPost]
        public IHttpActionResult CreateDiscount(DiscountDto discount)
        {
            try
            {
                var result = DiscountTasks.CreateDiscount(discount);

                return Ok(result);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}