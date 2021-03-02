using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class UserRepositoryTests : BaseRepositoryTests<User>
    {
        private readonly UserRepository _repository;

        private readonly User _user;

        public UserRepositoryTests()
        {
            _repository = new UserRepository(Context.Object);
            _user = new User
            {
                Id = Guid.NewGuid(),
                CategoryNotifications = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                AllNotificationsAreOn = true,
                Email = "abc@mail.com",
                Favorites = new List<Favorites>(),
                HotDiscountsNotificationIsOn = false,
                IsActive = true,
                Name = "Mary",
                NewDiscountNotificationIsOn = true,
                NewVendorNotificationIsOn = true,
                TagNotifications = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorNotifications = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                Role = UserRole.Employee,
                Address = new Address
                {
                    CityId = Guid.NewGuid(),
                    CountryId = Guid.NewGuid(),
                    Street = "g"
                },
                Password = "abc"
            };

            var usersWithSubscriptions = new List<User>();

            Context.Setup(c => c.GetAnyInAndWhereAsync(It.IsAny<Expression<Func<User, IEnumerable<Guid>>>>(),
                    It.IsAny<IEnumerable<Guid>>(), It.IsAny<Expression<Func<User, bool>>>()))
                    .Callback((
                        Expression<Func<User, IEnumerable<Guid>>> inField,
                        IEnumerable<Guid> subscriptions,
                        Expression<Func<User, bool>> expression) =>
                    {
                        var users = Collection.Where(expression.Compile()).ToList();
                        var currentSubscriptions = users.Select(inField.Compile());
                        var zipped = users.Zip(currentSubscriptions);

                        foreach (var userSubscription in zipped)
                        {
                            if (userSubscription.Second.Any(id => subscriptions.ToList().Contains(id)))
                            {
                                usersWithSubscriptions.Add(userSubscription.First);
                            }
                        }
                    })
                    .Returns(Task.FromResult((IEnumerable<User>)usersWithSubscriptions));

            Context.Setup(c => c.GetAnyEqAndWhereAsync(It.IsAny<Expression<Func<User, IEnumerable<Guid>>>>(),
                    It.IsAny<Guid>(), It.IsAny<Expression<Func<User, bool>>>()))
                .Callback((
                    Expression<Func<User, IEnumerable<Guid>>> inField,
                    Guid subscription,
                    Expression<Func<User, bool>> expression) =>
                {
                    var users = Collection.Where(expression.Compile()).ToList();
                    var currentSubscriptions = users.Select(inField.Compile());
                    var zipped = users.Zip(currentSubscriptions);

                    foreach (var userSubscription in zipped)
                    {
                        if (userSubscription.Second.Any(id => id == subscription))
                        {
                            usersWithSubscriptions.Add(userSubscription.First);
                        }
                    }
                })
                .Returns(Task.FromResult((IEnumerable<User>)usersWithSubscriptions));
        }

        [Fact]
        public void CanGet()
        {
            Collection.Add(_user);

            var result = _repository.Get();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Collection.Add(_user);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Collection.Add(_user);

            var result = await _repository.GetByIdAsync(_user.Id);
            Assert.Equal(_user, result);
        }

        [Fact]
        public async Task CanGetByEmailAsync()
        {
            Collection.Add(_user);

            var result = await _repository.GetByEmail(_user.Email);
            Assert.Equal(_user, result);
        }

        [Fact]
        public async Task CanGetWithSubscriptionsAsync()
        {
            Collection.Add(_user);
            var result = await _repository.GetWithSubscriptionsAsync(u => u.VendorNotifications,
                _user.VendorNotifications, u => u.IsActive);
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetWithSubscriptionAsync()
        {
            Collection.Add(_user);
            var result = await _repository.GetWithSubscriptionAsync(u => u.VendorNotifications,
                _user.VendorNotifications[0], u => u.IsActive);
            Assert.Single(result);
        }

        [Fact]
        public async Task CanUpdateManyAsync()
        {
            Collection.Add(_user.DeepClone());
            var user = _user.DeepClone();
            user.Id = Guid.NewGuid();
            Collection.Add(user.DeepClone());

            _user.CategoryNotifications.RemoveAt(0);
            user.VendorNotifications.RemoveAt(1);

            await _repository.UpdateManyAsync(new List<User> { user, _user });
            Assert.Single(Collection.Single(x => x.Id == _user.Id).CategoryNotifications);
            Assert.Single(Collection.Single(x => x.Id == user.Id).VendorNotifications);
        }
    }
}