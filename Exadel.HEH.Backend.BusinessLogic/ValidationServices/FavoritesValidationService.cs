using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Http;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class FavoritesValidationService : IFavoritesValidationService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserProvider _userProvider;
        private readonly IMethodProvider _methodProvider;

        public FavoritesValidationService(IDiscountRepository discountRepository, IUserRepository userRepository, IUserProvider userProvider, IMethodProvider methodProvider)
        {
            _discountRepository = discountRepository;
            _userRepository = userRepository;
            _userProvider = userProvider;
            _methodProvider = methodProvider;
        }

        public async Task<bool> ValidateDiscountIdIsExist(Guid discountId, CancellationToken token)
        {
            var result = await _discountRepository.GetByIdAsync(discountId);
            return !(result is null);
        }

        public async Task<bool> ValidateUserFavoritesIsExist(Guid discountId, CancellationToken token)
        {
            var user = await _userRepository.GetByIdAsync(_userProvider.GetUserId());
            var result = user.Favorites.FirstOrDefault(f => f.DiscountId == discountId);
            if (_methodProvider.GetMethodUpperName() == "POST")
            {
                return result is null;
            }
            else if (_methodProvider.GetMethodUpperName() == "PUT")
            {
                return !(result is null);
            }

            return false;
        }
    }
}
