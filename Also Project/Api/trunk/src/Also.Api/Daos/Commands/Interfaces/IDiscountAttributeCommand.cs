using Aafp.Also.Api.Models;
using System;

namespace Aafp.Also.Api.Daos.Commands.Interfaces
{
    public interface IDiscountAttributeCommand
    {
        PriceAttribute GetByKey(Guid Key);

        void Store(PriceAttribute priceAttribute);
    }
}