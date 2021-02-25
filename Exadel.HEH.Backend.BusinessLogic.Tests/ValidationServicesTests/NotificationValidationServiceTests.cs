using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidationServicesTests
{
    public class NotificationValidationServiceTests
    {
        private readonly NotificationValidationService _validationService;
        private readonly Notification _notification;

        public NotificationValidationServiceTests()
        {
            var notificationRepository = new Mock<INotificationRepository>();
            var notificationData = new List<Notification>();

            _notification = new Notification
            {
                Id = Guid.NewGuid()
            };

            notificationData.Add(_notification);

            notificationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(notificationData.FirstOrDefault(n => n.Id == id)));

            _validationService = new NotificationValidationService(notificationRepository.Object);
        }

        [Fact]
        public async Task CanValidateNotificationExists()
        {
            Assert.True(await _validationService.NotificationExistsAsync(_notification.Id));
        }
    }
}