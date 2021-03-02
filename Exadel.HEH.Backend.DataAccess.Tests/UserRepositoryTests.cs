using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class UserRepositoryTests : MongoRepositoryTests<User>
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