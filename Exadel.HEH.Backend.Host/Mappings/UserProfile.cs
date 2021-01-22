using AutoMapper;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.DTOs.Get;
using Exadel.HEH.Backend.Host.DTOs.Update;

namespace Exadel.HEH.Backend.Host.Mappings
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