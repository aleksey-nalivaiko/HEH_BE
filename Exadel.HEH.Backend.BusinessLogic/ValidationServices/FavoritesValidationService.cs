using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class FavoritesValidationService : IFavoritesValidationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProvider _userProvider;
        private readonly IMethodProvider _methodProvider;

        public FavoritesValidationService(IUserRepository userRepository, IUserProvider userProvider, IMethodProvider methodProvider)
        {
            _userRepository = userRepository;
            _userProvider = userProvider;
            _methodProvider = methodProvider;
        }

        public async Task<bool> UserFavoritesNotExists(Guid discountId, CancellationToken token)
        {
            var user = await _userRepository.GetByIdAsync(_userProvider.GetUserId());
            var result = user.Favorites.FirstOrDefault(f => f.DiscountId == discountId);

            if (_methodProvider.GetMethodUpperName() == "POST")
            {
                return result is null;
            }

            if (_methodProvider.GetMethodUpperName() == "PUT"
                     || _methodProvider.GetMethodUpperName() == "DELETE")
            {
                return !(result is null);
            }

            return true;
        }
    }
}
