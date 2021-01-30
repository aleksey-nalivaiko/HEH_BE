using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class FavoritesServiceTests : BaseServiceTests<User>
    {
        private readonly FavoritesService _service;
        private User _user;
        private Favorites _favorites;

        public FavoritesServiceTests()
        {
            var userProvider = new Mock<IUserProvider>();
            var repository = new Mock<IUserRepository>();
            _service = new FavoritesService(repository.Object, Mapper, userProvider.Object);

            InitTestData();
            userProvider.Setup(p => p.GetUserId()).Returns(_user.Id);
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_user);
            var result = await _service.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            Data.Add(_user);
            var newFavorites = new FavoritesDto
            {
                DiscountId = Guid.NewGuid(),
                Note = "Note2"
            };

            await _service.CreateAsync(newFavorites);
            var favorites = Data.Single(u => u.Id == _user.Id)
                .Favorites.FirstOrDefault(f => f.DiscountId == newFavorites.DiscountId);
            Assert.NotNull(favorites);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Data.Add(_user);
            var newFavorites = new FavoritesDto
            {
                DiscountId = _favorites.DiscountId,
                Note = "ChangedNote"
            };

            await _service.UpdateAsync(newFavorites);
            Assert.NotEqual(Data.Single(u => u.Id == _user.Id)
                .Favorites.Single(f => f.DiscountId == _favorites.DiscountId).Note, _favorites.Note);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_user);
            await _service.RemoveAsync(_favorites.DiscountId);
            Assert.Empty(Data.Single(u => u.Id == _user.Id).Favorites);
        }

        private void InitTestData()
        {
            _favorites = new Favorites
            {
                DiscountId = Guid.NewGuid(),
                Note = "Note1"
            };
            _user = new User
            {
                Id = Guid.NewGuid(),
                CategoryNotifications = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                CityChangeNotificationIsOn = true,
                Email = "abc@mail.com",
                Favorites = new List<Favorites> { _favorites },
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
    }
}