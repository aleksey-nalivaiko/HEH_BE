using System;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        private readonly IUserProvider _userProvider;

        public UserService(IUserRepository repository, IMapper mapper, IUserProvider userProvider)
            : base(repository, mapper)
        {
            _userProvider = userProvider;
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var result = await Repository.GetByIdAsync(id);
            return Mapper.Map<UserDto>(result);
        }

        public Task<UserDto> GetProfileAsync()
        {
            return GetByIdAsync(_userProvider.GetUserId());
        }

        public async Task UpdateStatusAsync(Guid id, bool isActive)
        {
            var user = await Repository.GetByIdAsync(id);
            user.IsActive = isActive;
            await Repository.UpdateAsync(user);
        }

        public async Task UpdateRoleAsync(Guid id, UserRole role)
        {
            var user = await Repository.GetByIdAsync(id);
            user.Role = role;
            await Repository.UpdateAsync(user);
        }
    }
}