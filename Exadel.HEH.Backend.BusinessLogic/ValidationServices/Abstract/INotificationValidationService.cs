using System;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface INotificationValidationService
    {
        Task<bool> NotificationExistsAsync(Guid id);
    }
}