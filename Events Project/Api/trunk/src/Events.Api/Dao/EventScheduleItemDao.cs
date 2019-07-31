using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao
{
    public class EventScheduleItemDao : GenericDao<EventScheduleItem>, IEventScheduleItemDao
    {
        public List<EventScheduleItem> GetByEvent(string eventCode)
        {
            var query = Session.GetNamedQuery("GetEventScheduleItems")
                .SetParameter("EventCode", eventCode);

            return query.List<EventScheduleItem>().ToList();
        }
    }
}