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
    public class UserServiceTests : BaseServiceTests<User>
    {
        private readonly UserService _service;

        private readonly User _user;

        public UserServiceTests()
        {
            _service = new UserService(Repository.Object, Mapper);
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
        public async Task CanGetAllAsync()
        {
            Data.Add(_user);
            var result = await _service.GetAllAsync();
            Assert.Single(result);
        }

        //[Fact]
        //public async Task CanGetById()
        //{
        //    Data.Add(_user);
        //    var result = await _service.GetByIdAsync(_user.Id);
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public async Task CanUpdate()
        //{
        //    Data.Add(_user.DeepClone());
        //    _user.IsActive = false;

        //    await _service.UpdateAsync(_user);
        //    Assert.False(Data.Single(x => x.Id == _user.Id).IsActive);
        //}
    }
}