using System;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface ICategoryValidationService
    {
        Task<bool> CategoryNotInDiscounts(Guid id, CancellationToken token = default);
    }
}
