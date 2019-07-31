using System;
using Aafp.Events.Api.Models;

namespace Aafp.Events.Api.Dao.Interfaces
{
    public interface IEditedRegistrationDao
    {
        EditedRegistration GetByKey(Guid key);

        EditedRegistration GetByEventKey(Guid eventKey, Guid customerKey);

        EditedRegistration GetByRegistrationKey(Guid registrationKey);

        void Store(EditedRegistration registration);

        EditedRegistration GetByCustomerEvent(Guid eventKey, Guid customerKey);
    }
}