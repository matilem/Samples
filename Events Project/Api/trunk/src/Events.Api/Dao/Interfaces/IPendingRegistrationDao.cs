using System;
using System.Collections.Generic;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IPendingRegistrationDao
    {
        PendingRegistration GetByKey(Guid key);

        List<PendingRegistration> GetByParentKey(Guid parentKey);

        PendingRegistration GetByEventKey(Guid eventKey, Guid customerKey);

        PendingRegistration GetByCustomerEvent(Guid eventKey, Guid customerKey);

        void Store(PendingRegistration registration);

        void Delete(PendingRegistration registration);

    }
}
