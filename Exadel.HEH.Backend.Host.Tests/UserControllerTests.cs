using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
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
            var validationService = new Mock<IUserValidationService>();
            _controller = new UserController(userService.Object, validationService.Object);
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
                }
            };
            userService.Setup(s => s.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<UserDto>)Data));

            userService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(() => Task.FromResult(Data.Single()));

            userService.Setup(s => s.GetProfileAsync())
                .Returns(() => Task.FromResult(Data.Single()));

            userService.Setup(s => s.UpdateStatusAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Callback((Guid id, bool isActive) =>
                {
                    var oldItem = Data.FirstOrDefault(x => x.Id == id);
                    if (oldItem != null)
                    {
                        Data.Remove(oldItem);
                        oldItem.IsActive = isActive;
                        Data.Add(oldItem);
                    }
                });

            userService.Setup(s => s.UpdateRoleAsync(It.IsAny<Guid>(), It.IsAny<UserRole>()))
                .Callback((Guid id, UserRole role) =>
                {
                    var oldItem = Data.FirstOrDefault(x => x.Id == id);
                    if (oldItem != null)
                    {
                        Data.Remove(oldItem);
                        oldItem.Role = role;
                        Data.Add(oldItem);
                    }
                });
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
            var result = await _controller.GetProfileAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanUpdateStatusAsync()
        {
            Data.Add(_user.DeepClone());
            _user.IsActive = false;

            await _controller.UpdateStatusAsync(_user.Id, _user.IsActive);
            Assert.False(Data.Single(x => x.Id == _user.Id).IsActive);
        }

        [Fact]
        public async Task CanUpdateRoleAsync()
        {
            Data.Add(_user.DeepClone());
            _user.Role = UserRole.Moderator;

            await _controller.UpdateRoleAsync(_user.Id, _user.Role);
            Assert.Equal(UserRole.Moderator, Data.Single(x => x.Id == _user.Id).Role);
        }
    }
}