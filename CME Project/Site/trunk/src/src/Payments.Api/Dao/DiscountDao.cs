using System;
using System.Collections.Generic;
using System.Linq;
using Aafp.Payments.Api.Dao.Interfaces;
using Aafp.Payments.Api.Models;

namespace Aafp.Payments.Api.Dao
{
    public class DiscountDao : GenericDao<DiscountDao>, IDiscountDao
    {
        public Discount GetDiscountByKey(Guid priceKey)
        {
            var query = Session.GetNamedQuery("GetDiscountByKey")
                .SetParameter("priceKey", priceKey);

            return query.UniqueResult<Discount>();
        }

        public Discount GetDiscountByCode(Guid priceKey, string code)
        {
            var query = Session.GetNamedQuery("GetDiscountByCode")
                .SetParameter("priceKey", priceKey)
                .SetParameter("priceCode", code);

            return query.UniqueResult<Discount>();
        }

        public List<Discount> GetAdminEventDiscountsByEventPrice(Guid priceKey)
        {
            var query = Session.GetNamedQuery("GetEventDiscountsForAdmin")
                .SetParameter("priceKey", priceKey);

            return query.List<Discount>().ToList();
        }
    }
}