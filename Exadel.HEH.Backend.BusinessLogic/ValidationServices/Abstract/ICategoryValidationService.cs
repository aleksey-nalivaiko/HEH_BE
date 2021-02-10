using System;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface ICategoryValidationService
    {
        Task<bool> CategoryNotInDiscountsAsync(Guid id);

        Task<bool> CategoryExistsAsync(Guid categoryId, CancellationToken token = default);

        Task<bool> CategoryNotExistsAsync(Guid categoryId, CancellationToken token = default);
    }
}
