using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class UserServiceTests : BaseServiceTests<User>
    {
        private readonly UserService _userService;

        private readonly User _user;

        public UserServiceTests()
        {
            var repository = new Mock<IUserRepository>();
            var userProvider = new Mock<IUserProvider>();

            _userService = new UserService(repository.Object, Mapper, userProvider.Object);
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

            userProvider.Setup(p => p.GetUserId()).Returns(_user.Id);

            repository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.Single()));

            repository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<User>)Data));
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_user);
            var result = await _userService.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Data.Add(_user);
            var result = await _userService.GetByIdAsync(_user.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetProfileAsync()
        {
            Data.Add(_user);
            var result = await _userService.GetProfile();
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task CanUpdate()
        //{
        //    Data.Add(_user.DeepClone());
        //    _user.IsActive = false;

        //    await _userService.UpdateAsync(_user);
        //    Assert.False(Data.Single(x => x.Id == _user.Id).IsActive);
        //}
    }
}