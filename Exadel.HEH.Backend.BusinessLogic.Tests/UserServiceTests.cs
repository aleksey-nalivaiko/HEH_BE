using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess;
using Exadel.HEH.Backend.DataAccess.Models;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class UserServiceTests : ServiceTests<User>
    {
        private readonly UserService _service;

        private readonly User _user;

        public UserServiceTests()
        {
            _service = new UserService(Repository.Object);
            _user = new User
            {
                Id = Guid.NewGuid(),
                CategoryNotificationsId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                CityChangeNotificationIsOn = true,
                Email = "abc@mail.com",
                Favorites = new List<Favorites>(),
                HotDiscountsNotificationIsOn = false,
                IsActive = true,
                Name = "Mary",
                NewDiscountNotificationIsOn = true,
                NewVendorNotificationIsOn = true,
                TagNotificationsId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorNotificationsId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                Role = UserRole.Employee,
                Office = new Address
                {
                    City = "m",
                    Country = "b",
                    Street = "g"
                },
                Password = "abc"
            };
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_user);
            var result = await _service.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Data.Add(_user);
            var result = await _service.GetByIdAsync(_user.Id);
            Assert.Equal(_user, result);
        }

        [Fact]
        public async Task CanUpdate()
        {
            Data.Add(_user.DeepClone());
            _user.IsActive = false;

            await _service.UpdateAsync(_user.Id, _user);
            Assert.False(Data.Single(x => x.Id == _user.Id).IsActive);
        }
    }
}