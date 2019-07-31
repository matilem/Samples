using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models.Badges;

namespace Aafp.Events.Api.Tasks.Admin.Interfaces
{
    public interface IAdminBadgeTasks
    {
        IEventDao EventDao { get; set; }

        IGuestDao GuestDao { get; set; }

        IRegistrantDao RegistrantDao { get; set; }

        IRegistrantSessionDao RegistrantSessionDao { get; set; }

        ISessionDao SessionDao { get; set; }

        BadgeBase GetUnsoldSessionBadge(Guid sessionKey);

        BadgeBase GetRegistrantBadge(Guid registrantkey);

        List<BadgeBase> GetAllRegistrantBadges(Guid registrantkey);

        List<BadgeBase> GetRegistrantSessionBadges(Guid registrantKey);

        List<BadgeBase> GetRegistrantSessionBadge(Guid registrantSessionKey);

        List<BadgeBase> GetEventBadges(Guid eventKey, DateTime? startDate, DateTime? endDate);
    }
}
