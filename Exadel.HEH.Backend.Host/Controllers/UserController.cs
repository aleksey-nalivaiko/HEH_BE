using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class UserController /*: BaseController<User, UserDto, UserUpdateDto, UserUpdateDto>*/
    {
        public UserController(IService<User> service, IMapper mapper)
            //: base(service, mapper)
        {
        }
    }
}