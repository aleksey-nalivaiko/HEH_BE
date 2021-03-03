using System;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IFavoritesValidationService
    {
        Task<bool> UserFavoritesNotExists(Guid discountId, CancellationToken token = default);
    }
}
