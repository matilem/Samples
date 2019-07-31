using System;
using System.Collections.Generic;
using Aafp.Events.Api.Dtos.Session;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface ISessionCoRequisiteGroupDao
    {
        List<SessionCoRequisiteGroupDto> GetSessionsCoRequisiteBySessionKey(Guid sessionKey);
    }
}
