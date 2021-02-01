using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class FavoritesValidationService : IFavoritesValidationService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserProvider _userProvider;

        public FavoritesValidationService(IDiscountRepository discountRepository, IUserRepository userRepository, IUserProvider userProvider)
        {
            _discountRepository = discountRepository;
            _userRepository = userRepository;
            _userProvider = userProvider;
        }

        public async Task<bool> ValidateDiscountId(Guid discountId, CancellationToken token)
        {
            var result = await _discountRepository.GetByIdAsync(discountId);
            return !(result is null);
        }

        public async Task<bool> ValidateUserFavorites(Guid discountId, CancellationToken token)
        {
            var user = await _userRepository.GetByIdAsync(_userProvider.GetUserId());
            var result = user.Favorites.FirstOrDefault(f => f.DiscountId == discountId);
            return result is null;
        }
    }
}
