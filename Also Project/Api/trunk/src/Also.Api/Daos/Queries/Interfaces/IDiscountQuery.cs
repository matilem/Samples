using Aafp.Also.Api.Dtos;
using System;

namespace Aafp.Also.Api.Daos.Queries.Interfaces
{
    public interface IDiscountQuery
    {
        Guid GetDiscount(string priceCode);
    }
}
