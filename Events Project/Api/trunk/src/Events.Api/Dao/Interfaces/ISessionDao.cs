using System;
using System.Collections.Generic;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface ISessionDao
    {
        Session GetByKey(Guid key);

        ////IList<Session> GetAll();

        int GetNumberOfRegisteredTickets(Guid sessionKey);

        List<string[]> GetSessionRoom(Guid sessionId);

        List<Session> GetSessionsForHeading(Guid headingKey);

        bool IncreaseSessionCapacity(Guid sessionKey);

        bool UpdateRegistrantGuestCapacity(Guid registrantSessionKey, decimal qty);
    }
}
