using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IUserService : IService<UserDto>
    {
        Task<UserDto> GetByIdAsync(Guid id);

        Task<UserDto> GetProfile();
    }
}