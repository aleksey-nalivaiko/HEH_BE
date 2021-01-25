using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers;
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
            _controller = new UserController(Service.Object);
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

            //_userDto = new UserUpdateDto
            //{
            //    Id = _user.Id,
            //    IsActive = true,
            //    UserRole = UserRole.Employee
            //};
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_user);
            var result = await _controller.GetAllAsync();
            Assert.Single(result);
        }

        //[Fact]
        //public async Task CanGetById()
        //{
        //    Data.Add(_user);
        //    var result = await _controller.GetByIdAsync(_user.Id);
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task CanUpdate()
        //{
        //    Data.Add(_user);
        //    _userDto.IsActive = false;

        //    await _controller.UpdateAsync(_userDto);
        //    Assert.False(Data.Single(x => x.Id == _user.Id).IsActive);
        //}
    }
}