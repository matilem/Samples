using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class PendingRegistrationDao : GenericDao<PendingRegistration>, IPendingRegistrationDao
    {
        public List<PendingRegistration> GetByParentKey(Guid parentKey)
        {
            return Session.Query<PendingRegistration>().Where(x => x.ParentRegistrationKey == parentKey).ToList();
        }

        public PendingRegistration GetByEventKey(Guid eventKey, Guid customerKey)
        {
            var pendingRegistrants =  Session.Query<PendingRegistration>().Where(x => x.EventKey == eventKey && x.CustomerKey == customerKey).OrderByDescending(x => x.AddDate).ToList();

            return pendingRegistrants.FirstOrDefault();
        } 

        public PendingRegistration GetByCustomerEvent(Guid eventKey, Guid customerKey)
        {
            var pendingRegistration = Session.Query<PendingRegistration>().Where(x => x.EventKey == eventKey && x.CustomerKey == customerKey).ToList();
            return pendingRegistration.FirstOrDefault();
        }

        public new void Store(PendingRegistration registration)
        {
            base.Store(registration);
            Session.Flush();
        }

        public new void Delete(PendingRegistration registration)
        {
            base.Delete(registration);
            Session.Flush();
        }
    }
}