using System;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class RegistrantOnWaitDao : GenericDao<RegistrantOnWait>, IRegistrantOnWaitDao
    {
        public IQueryable<RegistrantOnWait> GetByEventKey(Guid eventKey)
        {
            return Session.Query<RegistrantOnWait>().Where(r => r.Event.Key == eventKey);
        }
    }
}