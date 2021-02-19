using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Mappings
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, DiscountShortDto>()
                .ForMember(dest => dest.AddressesIds, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Addresses, opts => opts.Ignore());

            CreateMap<Discount, DiscountDto>()
                .ForMember(dest => dest.IsFavorite, opts => opts.Ignore());

            CreateMap<Discount, DiscountStatisticsDto>()
                .ForMember(dest => dest.ViewsAmount, opts => opts.Ignore());

            CreateMap<Discount, DiscountExtendedDto>()
                .ForMember(dest => dest.IsFavorite, opts => opts.Ignore())
                .ForMember(dest => dest.Links, opts => opts.Ignore())
                .ForMember(dest => dest.WorkingHours, opts => opts.Ignore())
                .ForMember(dest => dest.Phones, opts => opts.Ignore());
        }
    }
}