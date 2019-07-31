using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Aafp.Events.Api.Tasks.Interfaces;

namespace Aafp.Events.Api.Controllers
{
    [RoutePrefix("events")]
    public class EventController : ApiController
    {
        public IEventTasks EventTasks { get; set; }

        [Route("active")]
        public HttpResponseMessage GetActiveEvents()
        {
            var events = EventTasks.GetActiveEvents();
            return Request.CreateResponse(HttpStatusCode.OK, events);
        }

        [Route("{eventKey}")]
        public IHttpActionResult GetEvent(Guid eventKey)
        {
            var item = EventTasks.GetEventByKey(eventKey);

            if (item != null)
                return Ok(item);
            else
                return BadRequest("The system is unable to find the requested event.");
        }

        [Route("{eventCode}/schedule")]
        public IHttpActionResult GetEventSchedule(string eventCode)
        {
            var schedule = EventTasks.GetEventSchedule(eventCode);

            if (schedule != null)
                return Ok(schedule);
            else
                return BadRequest($"The system is unable to create a schedule for the event: {eventCode}.");
        }
    }
}
