using System.Collections.Generic;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IEventScheduleItemDao
    {
        List<EventScheduleItem> GetByEvent(string eventCode);
    }
}
