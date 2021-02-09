using System;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class UserValidationService : IUserValidationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProvider _userProvider;

        public UserValidationService(IUserRepository userRepository, IUserProvider userProvider)
        {
            _userRepository = userRepository;
            _userProvider = userProvider;
        }

        public async Task<bool> ValidateUserIdExists(Guid userId, CancellationToken token)
        {
            var result = await _userRepository.GetByIdAsync(userId);
            return !(result is null);
        }
    }
}