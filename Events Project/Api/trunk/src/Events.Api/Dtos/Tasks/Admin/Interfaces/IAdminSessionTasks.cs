using System;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos.Session;

namespace Aafp.Events.Api.Tasks.Admin.Interfaces
{
    public interface IAdminSessionTasks
    {
        ISessionDao SessionDao { get; set; }

        SessionDto GetSessionByKey(Guid sessionKey);

        bool IncreaseSessionCapacity(Guid sessionKey);
    }
}
