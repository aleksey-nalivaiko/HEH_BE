using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Update;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<Favorites, FavoritesDto>().ReverseMap();

            CreateMap<UserUpdateDto, User>();
        }
    }
}