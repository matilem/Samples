using Aafp.Also.Api.Models;
using System;

namespace Aafp.Also.Api.Daos.Commands.Interfaces
{
    public interface IDiscountCommand
    {
        Price GetByKey(Guid Key);

        void Store(Price price);
    }
}