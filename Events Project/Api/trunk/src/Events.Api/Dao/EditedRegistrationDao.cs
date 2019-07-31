using System;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class EditedRegistrationDao : GenericDao<EditedRegistration>, IEditedRegistrationDao
    {
        public EditedRegistration GetByEventKey(Guid eventKey, Guid customerKey)
        {
            var editedRegistrants = Session.Query<EditedRegistration>().Where(x => x.EventKey == eventKey && x.CustomerKey == customerKey).OrderByDescending(x => x.AddDate).ToList();

            return editedRegistrants.FirstOrDefault();
        }

        public EditedRegistration GetByCustomerEvent(Guid eventKey, Guid customerKey)
        {
            var editedRegistration = Session.Query<EditedRegistration>().Where(x => x.EventKey == eventKey && x.CustomerKey == customerKey).ToList();
            return editedRegistration.FirstOrDefault();
        }

        public EditedRegistration GetByRegistrationKey(Guid registrationKey)
        {
            var editedRegistration = Session.Query<EditedRegistration>().Where(x => x.RegistrantKey == registrationKey).ToList();
            return editedRegistration.FirstOrDefault();
        }

        public new void Store(EditedRegistration registration)
        {
            base.Store(registration);
            Session.Flush();
        }
    }
}