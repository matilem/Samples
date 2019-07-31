using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.User.Registration;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IRegistrantDao
    {
        Registrant GetRegistrant(Guid eventKey, Guid customerKey);

        Registrant GetRegistrantByKey(Guid registrantKey);

        bool IsRegisteredForEvent(Guid eventKey, Guid customerKey);

        List<Registrant> GetRegistrantsForEvent(Guid eventKey);

        List<Registrant> GetRegistrantsForEventByDate(Guid eventKey, DateTime startDate, DateTime endDate);

        int GetEventRegistrantsCount(Guid eventKey);

        IList<Guid> GetGuestsForRegistrant(Guid registrantKey);

        void Store(Registrant registrant);

        List<Registrant> GetRelatedRegistrations(Guid eventKey, Guid customerKey);

        List<UserRegistrationDto> GetUserRegistrationsForHomePage(Guid customerKey);
    }
}