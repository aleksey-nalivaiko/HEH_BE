using System;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IUserValidationService
    {
        Task<bool> ValidateUserIdExists(Guid userId, CancellationToken token = default);
    }
}