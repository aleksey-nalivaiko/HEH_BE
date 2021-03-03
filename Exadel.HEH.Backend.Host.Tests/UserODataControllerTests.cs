using System;
using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.OData;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class UserODataControllerTests :
        BaseControllerTests<UserShortDto>
    {
        private readonly UserController _controller;

        private readonly UserShortDto _user;

        public UserODataControllerTests()
        {
            var userService = new Mock<IUserService>();

            _controller = new UserController(userService.Object);

            userService.Setup(s => s.Get())
                .Returns(Data.AsQueryable());

            _user = new UserShortDto
            {
                Id = Guid.NewGuid(),
                Email = "abc@mail.com",
                IsActive = true,
                Name = "Mary",
                Role = UserRole.Employee,
                Address = new AddressDto
                {
                    CityId = Guid.NewGuid(),
                    CountryId = Guid.NewGuid(),
                    Street = "g"
                }
            };
        }

        [Fact]
        public void CanGet()
        {
            Data.Add(_user);
            var result = _controller.Get();
            Assert.Single(result);
        }
    }
}