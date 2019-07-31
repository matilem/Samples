using System;
using System.Collections.Generic;
using Aafp.Payments.Api.Models;

namespace Aafp.Payments.Api.Dao.Interfaces
{
    public interface IDiscountDao
    {
        Discount GetDiscountByKey(Guid priceKey);

        Discount GetDiscountByCode(Guid priceKey, string code);

        List<Discount> GetAdminEventDiscountsByEventPrice(Guid priceKey);
    }
}
