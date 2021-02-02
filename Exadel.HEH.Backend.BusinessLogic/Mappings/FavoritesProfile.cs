using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Mappings
{
    public class FavoritesProfile : Profile
    {
        public FavoritesProfile()
        {
            CreateMap<FavoritesDto, Favorites>()
                .ForMember(f => f.DiscountId, opt => opt.MapFrom(d => d.Id));

            CreateMap<DiscountDto, FavoritesDto>()
                .ForMember(f => f.Note, opt => opt.Ignore());
        }
    }
}