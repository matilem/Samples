using System;
using System.Collections.Generic;
using Aafp.Cme.Api.Dtos;

namespace Aafp.Cme.Api.Daos.Queries.Interfaces
{
    public interface ICreditAvailableQuery
    {
        List<CreditAvailableDto> GetPurchasedByCustomer(Guid customerKey);

        List<CreditAvailableDto> GetSubscriptionsByCustomer(Guid customerKey);
    }
}
