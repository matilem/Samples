using Aafp.Also.Api.Dtos;
using System;

namespace Aafp.Also.Api.Tasks.Interfaces
{
    public interface IDiscountTasks
    {
        Guid CreateDiscount(DiscountDto discount);
    }
}
