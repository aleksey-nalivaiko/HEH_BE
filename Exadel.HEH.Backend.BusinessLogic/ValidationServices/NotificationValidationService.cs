using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class NotificationValidationService : INotificationValidationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationValidationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> NotificationExistsAsync(Guid id)
        {
            var result = await _notificationRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}