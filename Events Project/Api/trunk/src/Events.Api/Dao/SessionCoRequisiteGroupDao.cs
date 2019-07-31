using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos.Session;
using Aafp.Events.Api.Models;
using AutoMapper.QueryableExtensions;
using NHibernate;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class SessionCoRequisiteGroupDao : ISessionCoRequisiteGroupDao
    {
        public ISession Sessions { get; set; }

        public List<SessionCoRequisiteGroupDto> GetSessionsCoRequisiteBySessionKey(Guid sessionKey)
        {
            var sessions = Sessions.Query<SessionCoRequisiteGroup>().Where(g => g.SessionKey == sessionKey).Project().To<SessionCoRequisiteGroupDto>().ToList();

            return sessions;
        }
    }
}