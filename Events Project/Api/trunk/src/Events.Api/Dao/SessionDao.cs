using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class SessionDao : GenericDao<Session>, ISessionDao
    {
        public int GetNumberOfRegisteredTickets(Guid sessionKey)
        {
            var sql = "EXEC client_aafp_event_count_registered_tickets_by_session :sessionKey";
            var query = Session.CreateSQLQuery(sql)
                .SetParameter("sessionKey", sessionKey.ToString());

            return query.UniqueResult<int>();
        }

        public List<string[]> GetSessionRoom(Guid sessionId)
        {
            var query = Session.GetNamedQuery("GetSessionRoom")
                .SetParameter("sesKey", sessionId);

            return (from object[] qr in query.List() select new[] { qr[0].ToString(), qr[1].ToString() }).ToList();
        }

        public List<Session> GetSessionsForHeading(Guid headingKey)
        {
            return Session.Query<Session>().Where(x => x.HeadingKey == headingKey).OrderBy(x => x.Sequence).ToList();
        }

        public bool IncreaseSessionCapacity(Guid sessionKey)
        {
            var sql = "UPDATE ev_session SET ses_capacity = ses_capacity + 1 WHERE ses_key = :@SessionKey";
            var query = Session.CreateSQLQuery(sql).SetParameter("@SessionKey", sessionKey);
            var result = query.ExecuteUpdate();

            return result == 1;
        }

        public bool UpdateRegistrantGuestCapacity(Guid registrantSessionKey, decimal qty)
        {
            var sql = "UPDATE ev_registrant_session SET rgs_qty = :@Qty WHERE rgs_key = :@registrantSessionKey";
            var query = Session.CreateSQLQuery(sql)
                .SetParameter("@Qty", qty)
                .SetParameter("@registrantSessionKey", registrantSessionKey);
            var result = query.ExecuteUpdate();

            return result == 1;
        }
    }
}