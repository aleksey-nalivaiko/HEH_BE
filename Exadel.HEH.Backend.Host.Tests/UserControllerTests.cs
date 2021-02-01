using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class UserControllerTests
        : BaseControllerTests<UserDto>
    {
        private readonly UserController _controller;

        private readonly UserDto _user;

        public UserControllerTests()
        {
            var userService = new Mock<IUserService>();
            _controller = new UserController(userService.Object);
            _user = new UserDto
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
            userService.Setup(s => s.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<UserDto>)Data));

            userService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(() => Task.FromResult(Data.Single()));

            userService.Setup(s => s.GetProfile())
                .Returns(() => Task.FromResult(Data.Single()));
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_user);
            var result = await _controller.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Data.Add(_user);
            var result = await _controller.GetByIdAsync(_user.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetProfileAsync()
        {
            Data.Add(_user);
            var result = await _controller.GetProfile();
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task CanUpdate()
        //{
        //    _data.Add(_user);
        //    _userDto.IsActive = false;

        //    await _controller.UpdateAsync(_userDto);
        //    Assert.False(_data.Single(x => x.Id == _user.Id).IsActive);
        //}
    }
}