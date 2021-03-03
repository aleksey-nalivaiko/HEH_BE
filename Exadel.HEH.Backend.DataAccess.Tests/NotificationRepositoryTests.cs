using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class NotificationRepositoryTests : BaseRepositoryTests<Notification>
    {
        private readonly INotificationRepository _repository;
        private Notification _notification;
        private IList<Notification> _notificationData;

        public NotificationRepositoryTests()
        {
            _repository = new NotificationRepository(Context.Object);
        }

        [Fact]
        public async Task CanGet()
        {
            Collection.Add(_notification);
            var result = await _repository.Get().ToListAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetAsync()
        {
            Collection.Add(_notification);
            var result = await _repository.GetAsync(notification => true);
            Assert.Single(result);
        }

        [Fact]
        public async Task CanCreateManyAsync()
        {
            await _repository.CreateManyAsync((IEnumerable<Notification>)_notificationData);
            var result = await _repository.Get().ToListAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanUpdateManyAsync()
        {
            await _repository.CreateManyAsync(_notificationData);
            _notificationData[0].IsRead = true;
            await _repository.UpdateManyAsync(_notificationData);
            var result = await _repository.Get().ToListAsync();
            Assert.True(result[0].IsRead);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Collection.Add(_notification);
            await _repository.RemoveAsync(n => n.Id == _notification.Id);
            var result = await _repository.Get().ToListAsync();
            Assert.Empty(result);
        }

        private void InitializeData()
        {
            _notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                IsRead = false,
                Date = DateTime.Now,
                Message = "Test message",
                SubjectId = Guid.NewGuid(),
                Title = "Test title",
                Type = NotificationType.Discount
            };

            _notificationData = new List<Notification> { _notification };
        }
    }
}