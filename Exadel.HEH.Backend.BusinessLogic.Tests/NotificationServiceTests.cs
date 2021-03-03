using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class NotificationServiceTests : BaseServiceTests<Notification>
    {
        private readonly NotificationService _service;
        private readonly IMapper _mapper;
        private User _currentUser;

        public NotificationServiceTests()
        {
            var notificationRepository = new Mock<INotificationRepository>();
            var logger = new Mock<ILogger<NotificationService>>();
            var userService = new Mock<IUserService>();
            var vendorSearchService = new Mock<IVendorSearchService>();
            var userProvider = new Mock<IUserProvider>();
            _mapper = MapperExtensions.Mapper;
            _service = new NotificationService(notificationRepository.Object, logger.Object, userService.Object,
                vendorSearchService.Object,
                userProvider.Object, _mapper);

            notificationRepository.Setup(r => r.Get())
                .Returns(() => Data.AsQueryable());
            notificationRepository.Setup(r =>
                    r.GetAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                .Returns(() => Task.FromResult((IEnumerable<Notification>)Data));
            notificationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(n => n.Id == id)));
            notificationRepository.Setup(r => r.CreateAsync(It.IsAny<Notification>()))
                .Callback((Notification item) => Data.Add(item))
                .Returns(Task.CompletedTask);
            notificationRepository.Setup(r => r.RemoveAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                .Callback(() => Data.RemoveAll(x => x.IsRead == false))
                .Returns(Task.CompletedTask);
            userProvider.Setup(p => p.GetUserId())
                .Returns(() => _currentUser.Id);

            InitializeData();
        }

        [Fact]
        public async Task CanGet()
        {
            var result = await _service.Get().ToListAsync();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CanGetNotReadAsync()
        {
            var result = await _service.GetNotReadCountAsync();
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task CanUpdateIsReadAsync()
        {
            var result = await Task.FromResult(_service.UpdateIsReadAsync(Data[0].Id));
            Assert.True(result.IsCompleted);
        }

        [Fact]
        public async Task CanUpdateAreReadAsync()
        {
            var result = await Task.FromResult(_service.UpdateAreReadAsync());
            Assert.True(result.IsCompleted);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            var resultVendor = await Task.FromResult(_service.RemoveVendorNotificationsAsync(Data[0].SubjectId));
            Data.Add(new Notification
            {
                Id = Guid.NewGuid(),
                UserId = _currentUser.Id,
                Date = DateTime.Now,
                IsRead = true,
                SubjectId = Guid.NewGuid(),
                Type = NotificationType.Discount
            });
            var resultDiscount = await Task.FromResult(_service.RemoveDiscountNotificationsAsync(Data[0].SubjectId));
            Assert.True(resultVendor.IsCompleted && resultDiscount.IsCompleted);
        }

        private void InitializeData()
        {
            _currentUser = new User
            {
                Id = Guid.NewGuid()
            };

            var notifications = new List<Notification>
            {
                new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = _currentUser.Id,
                    Date = DateTime.Now,
                    IsRead = false,
                    SubjectId = Guid.NewGuid(),
                    Type = NotificationType.Vendor
                },
                new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = _currentUser.Id,
                    Date = DateTime.Now,
                    IsRead = true,
                    SubjectId = Guid.NewGuid(),
                    Type = NotificationType.Discount
                },
                new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    IsRead = false
                },
            };

            Data.AddRange(notifications);
        }
    }
}