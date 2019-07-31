using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Events.Api.Dao.Interfaces;
using Aafp.Events.Api.Models;
using NHibernate;

namespace Aafp.Events.Api.Dao
{
    public class FeeDao : IFeeDao
    {
        public ISession Session { get; set; }

        public List<Fee> GetEventFeesByCustomerForAdmin(Guid eventKey, Guid customerKey, DateTime? registrationDate)
        {
            var query = Session.GetNamedQuery("GetEventFeesForCustomer")
                .SetParameter("CustomerKey", customerKey)
                .SetParameter("EventKey", eventKey)
                .SetParameter("RegistrationDate", registrationDate?.Date ?? DateTime.Now.Date);

            return query.List<Fee>().ToList();
        }

        public List<Fee> GetEventFeesByCustomer(Guid eventKey, Guid customerKey, bool isMember, DateTime? registrationDate)
        {
            var query = Session.GetNamedQuery("GetEventFeesForCustomer")
                .SetParameter("CustomerKey", customerKey)
                .SetParameter("EventKey", eventKey)
                .SetParameter("RegistrationDate", registrationDate?.Date ?? DateTime.Now.Date);

            var fees = query.List<Fee>().ToList();

            fees = fees.Where(x => x.SellOnline).ToList();

            //Temporary breaking returning the member lowest price from issues with PDRW, will remove 04/01/2017
            //if (isMember)
            //    fees = GetLowestEventFeeForCustomer(fees);

            return fees;
        }

        public List<Fee> GetSessionFeesForCustomer(Guid customerKey, Guid eventKey, DateTime? registrationDate)
        {
            var query = Session.GetNamedQuery("GetEventSessionFeesForCustomer")
                .SetParameter("CustomerKey", customerKey)
                .SetParameter("EventKey", eventKey)
                .SetParameter("RegistrationDate", registrationDate?.Date ?? DateTime.Now.Date);

            return query.List<Fee>().ToList();
        }

        private List<Fee> GetLowestEventFeeForCustomer(List<Fee> fees)
        {
            if (fees == null || fees.Count == 0)
                return null;

            var minFee = fees[0];

            foreach (var fee in fees)
            {
                if (fee.Price < minFee.Price)
                    minFee = fee;
            }

            return new List<Fee> { minFee };
        }

        public List<Fee> GetEventFeesForBatch(Guid eventKey)
        {
            var query = Session.GetNamedQuery("GetEventFees")
                .SetParameter("EventKey", eventKey);

            var fees = query.List<Fee>().ToList();

            fees = fees.Where(x => x.Price == Decimal.Zero).ToList();

            return fees;
        }
    }
}