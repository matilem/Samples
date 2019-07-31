using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;

namespace Aafp.Events.Api.Tasks.Interfaces
{
    public interface IEventTasks
    {
        IEventDao EventDao { get; set; }

        List<EventBaseDto> GetActiveEvents();

        EventDetailDto GetEventByKey(Guid eventKey);

        List<EventScheduleItemDto> GetEventSchedule(string eventCode);
    }
}
