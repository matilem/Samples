using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Dtos;
using Aafp.Events.Api.Models;
using AutoMapper.QueryableExtensions;
using NHibernate.Linq;

namespace Aafp.Events.Api.Dao
{
    public class EventDao : GenericDao<Event>, IEventDao
    {
        public EventBaseDto GetEventBaseByKey(Guid key)
        {
            var evt = Session.Query<Event>().Where(x => x.Key == key).Project().To<EventBaseDto>().ToList();

            return evt.FirstOrDefault();
        }

        public Event GetByCode(string eventCode)
        {
            var evt = Session.Query<Event>().Where(x => x.Code == eventCode).ToList();

            return evt.FirstOrDefault();
        }

        public List<EventBaseDto> GetActiveEvents()
        {
            return Session.Query<Event>().Where(x => DateTime.Today > x.PostToWebDate.Value && DateTime.Today < x.RemoveFromWebDate.Value).Project().To<EventBaseDto>().ToList();
        }

        public List<EventBaseDto> GetAdminRegistrationEvents()
        {
            return Session.Query<Event>().Where(x => x.StartDate.HasValue && x.StartDate.Value > DateTime.Today.AddMonths(-12)).Project().To<EventBaseDto>().ToList();
        }

        public List<CustomerEvent> GetCustomerEvents(string customerKeyList)
        {
            var query = Session.GetNamedQuery("GetCustomerEvents")
                .SetParameter("CustomerKeyList", customerKeyList);

            return query.List<CustomerEvent>().ToList();
        }

        public IList<Event> GetOrgAppEvents()
        {
            return Session.Query<Event>().Where(x => x.IsOrganizationalApplicationFlag).ToList();
        }

        public List<EventBaseDto> GetBatchEvents()
        {
            return Session.Query<Event>().Where(x => x.StartDate.HasValue && x.StartDate.Value > DateTime.Today.AddMonths(-24)).Project().To<EventBaseDto>().ToList();
        }

        public List<EventBaseDto> GetRelatedEvents(Guid eventKey)
        {
            var relatedEvents = new List<EventBaseDto>();
            var keys = Session.CreateSQLQuery("SELECT e43_related_evt_key FROM client_aafp_e43_related_event WITH (NOLOCK) WHERE e43_evt_key = :@EventKey")
                    .SetParameter("@EventKey", eventKey)
                    .List<Guid>();

            foreach (var key in keys)
            {
                var results = Session.Query<Event>().Where(x => x.Key == key).Project().To<EventBaseDto>().ToList();
                var evt = results.FirstOrDefault();

                if (evt != null)
                    relatedEvents.Add(evt);
            }

            return relatedEvents;
        }
    }
}