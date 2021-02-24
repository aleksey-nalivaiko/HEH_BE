using System;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IDiscountValidationService
    {
        Task<bool> DiscountExists(Guid discountId, CancellationToken token = default);

        Task<bool> DiscountNotExists(Guid discountId, CancellationToken token = default);
    }
}