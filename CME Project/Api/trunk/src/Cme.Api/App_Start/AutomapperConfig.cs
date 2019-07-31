using Aafp.Cme.Api.Dtos;
using Aafp.Cme.Api.Helpers;
using AutoMapper;

namespace Aafp.Cme.Api
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<AemCmeItemDto, CreditAvailableDto>()
                .ForMember(viewModel => viewModel.Title,
                options => options.MapFrom(record => record.Title))
                .ForMember(viewModel => viewModel.AccessUrl,
                options => options.MapFrom(record => $"{ApplicationConfig.AemBaseUrl}{record.Url}"))
                .ForMember(viewModel => viewModel.ProductImage,
                options => options.MapFrom(record => record.ThumbnailDesktop))
                .ForMember(viewModel => viewModel.CreditsAvailable,
                options => options.MapFrom(record => record.Credits))
                .ForMember(viemodel => viemodel.ClaimCreditUrl,
                options => options.MapFrom(record => record.CmeLink));

            Mapper.CreateMap<ReElectionTotalsHelper, ReElectionTotalsDto>();
            Mapper.CreateMap<CreditTypeReElectionTotalsHelper, CreditTypeReElectionTotalsDto>();
        }
    }
}