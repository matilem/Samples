using System;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos.Session;
using Aafp.Events.Api.Tasks.Admin.Interfaces;

namespace Aafp.Events.Api.Tasks.Admin
{
    public class AdminSessionTasks : IAdminSessionTasks
    {
        public ISessionDao SessionDao { get; set; }

        public SessionDto GetSessionByKey(Guid sessionKey)
        {
            var session = SessionDao.GetByKey(sessionKey);
            var dto = AutoMapper.Mapper.Map(session, new SessionDto());

            return dto;
        }

        public bool IncreaseSessionCapacity(Guid sessionKey)
        {
            return SessionDao.IncreaseSessionCapacity(sessionKey);
        }
    }
}