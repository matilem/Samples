using Aafp.Also.Api.Dtos;
using Aafp.Also.Api.Tasks.Interfaces;
using ApiClientHelper.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Aafp.Also.Api.Controllers
{
    [RoutePrefix("instructor")]
    public class InstructorController : ApiController
    {
        public IInstructorTasks InstructorTasks { get; set; }


        [Route("remove")]
        [HttpPost]
        public IHttpActionResult RemoveInstructor([FromBody] InstructorRemoveDto dto)
        {
            try
            {
                var result = InstructorTasks.RemoveInstructor(dto);

                return Ok(result);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
