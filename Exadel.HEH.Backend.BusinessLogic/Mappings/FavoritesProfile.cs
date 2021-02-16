using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Mappings
{
    public class FavoritesProfile : Profile
    {
        public FavoritesProfile()
        {
            CreateMap<FavoritesDto, Favorites>()
                .ForMember(f => f.DiscountId, opt => opt.MapFrom(d => d.Id))
                .ReverseMap();

            CreateMap<FavoritesShortDto, Favorites>().ReverseMap();

            CreateMap<DiscountDto, FavoritesDto>()
                .ForMember(f => f.Note, opt => opt.Ignore());
        }
    }
}