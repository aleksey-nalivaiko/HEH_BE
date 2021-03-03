using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class NotificationRepositoryTests : BaseRepositoryTests<Notification>
    {
        private readonly NotificationRepository _repository;
        private Notification _notification;
        private IList<Notification> _notificationData;

        public NotificationRepositoryTests()
        {
            _repository = new NotificationRepository(Context.Object);

            Context.Setup(c => c.CreateManyAsync(It.IsAny<IEnumerable<Notification>>()))
                .Callback((IEnumerable<Notification> docs) =>
                {
                    Collection.AddRange(docs);
                })
                .Returns(Task.CompletedTask);

            Context.Setup(c => c.UpdateManyAsync(It.IsAny<IEnumerable<Notification>>()))
                .Callback((IEnumerable<Notification> docs) =>
                {
                    foreach (var doc in docs)
                    {
                        var oldDoc = Collection.FirstOrDefault(x => x.Id == doc.Id);
                        if (oldDoc != null)
                        {
                            Collection.Remove(oldDoc);
                            Collection.Add(doc);
                        }
                    }
                })
                .Returns(Task.CompletedTask);

            InitializeData();
        }

        [Fact]
        public async Task CanGetAll()
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
            await _repository.CreateManyAsync(_notificationData);
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
            await _repository.RemoveAsync(x => x.Id == _notification.Id);
            Assert.Empty(Collection);
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