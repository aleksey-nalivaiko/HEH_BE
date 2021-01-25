using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                CityChangeNotificationIsOn = true,
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
        public async Task CanGetAll()
        {
            Collection.Add(_user);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Collection.Add(_user);

            var result = await _repository.GetByIdAsync(_user.Id);
            Assert.Equal(_user, result);
        }

        [Fact]
        public async Task CanUpdate()
        {
            Collection.Add(_user.DeepClone());
            _user.IsActive = false;

            await _repository.UpdateAsync(_user);
            Assert.False(Collection.Single(x => x.Id == _user.Id).IsActive);
        }
    }
}