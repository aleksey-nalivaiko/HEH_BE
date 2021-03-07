using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
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
        private User _user;
        private VendorSearch _vendor;
        private LocationDto _location;
        private Discount _discount;

        public NotificationServiceTests()
        {
            var notificationRepository = new Mock<INotificationRepository>();
            var logger = new Mock<ILogger<NotificationService>>();
            var userService = new Mock<IUserService>();
            var vendorSearchService = new Mock<IVendorSearchService>();
            var userProvider = new Mock<IUserProvider>();
            var mapper = MapperExtensions.Mapper;
            _service = new NotificationService(notificationRepository.Object, logger.Object, userService.Object,
                vendorSearchService.Object,
                userProvider.Object, mapper);

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

            notificationRepository.Setup(r => r.CreateManyAsync(It.IsAny<IEnumerable<Notification>>()))
                .Callback((IEnumerable<Notification> notifications) =>
                {
                    Data.AddRange(notifications);
                })
                .Returns(Task.CompletedTask);

            notificationRepository.Setup(r => r.RemoveAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                .Callback(() => Data.RemoveAll(x => x.IsRead == false))
                .Returns(Task.CompletedTask);

            userProvider.Setup(p => p.GetUserId())
                .Returns(() => _user.Id);

            InitializeData();

            var vendors = new List<VendorSearch> { _vendor };

            vendorSearchService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(vendors.First(v => v.Id == id)));

            var users = new List<User> { _user };

            userService.Setup(s => s.GetUsersWithNotificationsAsync(
                    It.IsAny<Guid>(), It.IsAny<IEnumerable<Guid>>(), It.IsAny<Guid>(),
                    It.IsAny<IList<Address>>(), It.IsAny<Expression<Func<User, bool>>>()))
                .Returns((Guid categoryId,
                    IEnumerable<Guid> tagIds,
                    Guid vendorId,
                    IList<Address> discountAddresses,
                    Expression<Func<User, bool>> expression) =>
                {
                    var foundUsers = users.Where(
                        u => u.CategoryNotifications.Contains(categoryId)
                             || u.VendorNotifications.Contains(vendorId)
                             || u.TagNotifications.Any(tagIds.Contains));

                    foundUsers = foundUsers.Where(expression.Compile());

                    var countryCities = discountAddresses
                        .GroupBy(a => a.CountryId)
                        .Select(g =>
                            new KeyValuePair<Guid, IEnumerable<Guid?>>(
                                g.Key, g.Select(a => a.CityId).Where(i => i.HasValue)))
                        .ToDictionary(a => a.Key, a => a.Value);

                    return Task.FromResult(foundUsers
                        .GroupBy(u => u.Id)
                        .Select(g => g.First())
                        .Where(u => countryCities.ContainsKey(u.Address.CountryId)
                                    && (!countryCities[u.Address.CountryId].Any()
                                        || countryCities[u.Address.CountryId].Contains(u.Address.CityId))));
                });

            userService.Setup(s => s.GetUsersWithNotificationsAsync(
                    It.IsAny<IEnumerable<Guid>>(), It.IsAny<IEnumerable<Guid>>(),
                    It.IsAny<IList<Address>>(), It.IsAny<Expression<Func<User, bool>>>()))
                .Returns((IEnumerable<Guid> categoriesIds,
                    IEnumerable<Guid> tagIds,
                    IList<Address> discountAddresses,
                    Expression<Func<User, bool>> expression) =>
                {
                    var foundUsers = users.Where(
                        u => u.CategoryNotifications.Any(categoriesIds.Contains)
                             || u.TagNotifications.Any(tagIds.Contains));

                    foundUsers = foundUsers.Where(expression.Compile());

                    var countryCities = discountAddresses
                        .GroupBy(a => a.CountryId)
                        .Select(g =>
                            new KeyValuePair<Guid, IEnumerable<Guid?>>(
                                g.Key, g.Select(a => a.CityId).Where(i => i.HasValue)))
                        .ToDictionary(a => a.Key, a => a.Value);

                    return Task.FromResult(foundUsers
                        .GroupBy(u => u.Id)
                        .Select(g => g.First())
                        .Where(u => countryCities.ContainsKey(u.Address.CountryId)
                                    && (!countryCities[u.Address.CountryId].Any()
                                        || countryCities[u.Address.CountryId].Contains(u.Address.CityId))));
                });
        }

        [Fact]
        public async Task CanCreateVendorNotificationsAsync()
        {
            await _service.CreateVendorNotificationsAsync(_vendor.Id);

            Assert.Equal(4, Data.Count);
            Assert.Equal(_vendor.Id, Data[3].SubjectId);
        }

        [Fact]
        public async Task CanCreateDiscountNotificationsAsync()
        {
            await _service.CreateDiscountNotificationsAsync(_discount);

            Assert.Equal(4, Data.Count);
            Assert.Equal(_discount.Id, Data[3].SubjectId);
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
                UserId = _user.Id,
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
            _location = new LocationDto
            {
                Id = Guid.NewGuid(),
                Cities = new List<CityDto>
                {
                    new CityDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Minsk"
                    }
                },
                Country = "Belarus"
            };

            var categoryId = Guid.NewGuid();
            var tagId = Guid.NewGuid();

            _user = new User
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                Address = new Address
                {
                    CityId = _location.Cities[0].Id,
                    CountryId = _location.Id,
                    Street = "g"
                },
                TagNotifications = new List<Guid> { tagId },
                CategoryNotifications = new List<Guid> { categoryId },
                AllNotificationsAreOn = true,
                NewVendorNotificationIsOn = true,
                NewDiscountNotificationIsOn = true
            };

            _vendor = new VendorSearch
            {
                Id = Guid.NewGuid(),
                Vendor = "Vendor",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Id = 1,
                        CityId = _location.Cities[0].Id,
                        CountryId = _location.Id,
                        Street = "street"
                    }
                },
                CategoriesIds = new List<Guid>
                {
                    categoryId
                },
                TagsIds = new List<Guid>
                {
                    tagId
                }
            };

            _discount = new Discount
            {
                Id = Guid.NewGuid(),
                Addresses = new List<Address>
                {
                    _vendor.Addresses.ElementAt(0)
                },
                CategoryId = categoryId,
                Conditions = "Conditions",
                TagsIds = new List<Guid> { tagId },
                VendorId = _vendor.Id,
                VendorName = _vendor.Vendor,
                PromoCode = "new promo code",
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date
            };

            var notifications = new List<Notification>
            {
                new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = _user.Id,
                    Date = DateTime.Now,
                    IsRead = false,
                    SubjectId = Guid.NewGuid(),
                    Type = NotificationType.Vendor
                },
                new Notification
                {
                    Id = Guid.NewGuid(),
                    UserId = _user.Id,
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