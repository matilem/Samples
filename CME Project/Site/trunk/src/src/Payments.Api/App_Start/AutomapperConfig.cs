using Aafp.Payments.Api.Dtos.Registration;
using Aafp.Payments.Api.Models;

namespace Aafp.Payments.Api
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<Discount, RegistrationDiscountDto>();
        }
    }
}