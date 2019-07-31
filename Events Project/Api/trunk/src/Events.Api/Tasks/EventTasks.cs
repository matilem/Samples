using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Tasks.Interfaces;

namespace Aafp.Events.Api.Tasks
{
    public class EventTasks : IEventTasks
    {
        public IEventDao EventDao { get; set; }

        public IEventScheduleItemDao EventScheduleItemDao { get; set; }

        public IFeeDao FeeDao { get; set; }

        public ISessionDao SessionDao { get; set; }

        public List<EventBaseDto> GetActiveEvents()
        {
            return EventDao.GetActiveEvents();
        }

        public EventDetailDto GetEventByKey(Guid eventKey)
        {
            var item = AutoMapper.Mapper.Map(EventDao.GetByKey(eventKey), new EventDetailDto());

            return item;
        }

        public List<EventScheduleItemDto> GetEventSchedule(string eventCode)
        {
            return AutoMapper.Mapper.Map(EventScheduleItemDao.GetByEvent(eventCode), new List<EventScheduleItemDto>());
        }
    }
}