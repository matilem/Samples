using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IEventDao
    {
        Event GetByKey(Guid eventKey);

        EventBaseDto GetEventBaseByKey(Guid key);

        Event GetByCode(string eventCode);

        List<EventBaseDto> GetActiveEvents();

        List<EventBaseDto> GetAdminRegistrationEvents();

        List<CustomerEvent> GetCustomerEvents(string customerKeyList);

        List<EventBaseDto> GetRelatedEvents(Guid eventKey);
    }
}
