using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Mappings
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, DiscountDto>()
                .ForMember(dest => dest.IsFavorite, opts => opts.Ignore())
                .ReverseMap();

            CreateMap<Discount, DiscountExtendedDto>()
                .ForMember(dest => dest.IsFavorite, opts => opts.Ignore())
                .ForMember(dest => dest.Links, opts => opts.Ignore())
                .ForMember(dest => dest.WorkingHours, opts => opts.Ignore())
                .ForMember(dest => dest.Addresses, opts => opts.Ignore())
                .ForMember(dest => dest.Phones, opts => opts.Ignore());
        }
    }
}