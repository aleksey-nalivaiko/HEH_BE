using System;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IUserValidationService
    {
        Task<bool> UserExists(Guid userId);
    }
}