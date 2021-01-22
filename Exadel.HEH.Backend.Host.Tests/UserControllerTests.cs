using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Update;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class UserControllerTests
        : BaseControllerTests<User>
    {
        private readonly UserController _controller;

        private readonly User _user;
        private readonly UserUpdateDto _userDto;

        public UserControllerTests()
        {
            _controller = new UserController(Service.Object, Mapper);
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

            _userDto = new UserUpdateDto
            {
                Id = _user.Id,
                IsActive = true,
                Role = UserRole.Employee
            };
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_user);
            var result = await _controller.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Data.Add(_user);
            var result = await _controller.GetByIdAsync(_user.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanUpdate()
        {
            Data.Add(_user);
            _userDto.IsActive = false;

            await _controller.UpdateAsync(_userDto);
            Assert.False(Data.Single(x => x.Id == _user.Id).IsActive);
        }
    }
}