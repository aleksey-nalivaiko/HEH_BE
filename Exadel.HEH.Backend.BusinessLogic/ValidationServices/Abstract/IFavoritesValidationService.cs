using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IFavoritesValidationService
    {
        Task<bool> ValidateDiscountId(Guid discountId, CancellationToken token);

        Task<bool> ValidateUserFavorites(Guid discountId, CancellationToken token);
    }
}
