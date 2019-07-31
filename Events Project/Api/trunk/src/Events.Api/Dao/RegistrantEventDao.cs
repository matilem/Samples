using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao
{
    public class RegistrantEventDao : GenericDao<CustomerEvent>, IRegistrantEventDao
    {
        public List<CustomerEvent> GetRegistrantEvents(Guid customerKey)
        {
            var query = Session.GetNamedQuery("GetRegistrantEvents")
                .SetParameter("CustomerKey", customerKey);

            return query.List<CustomerEvent>().ToList();
        }
    }
}