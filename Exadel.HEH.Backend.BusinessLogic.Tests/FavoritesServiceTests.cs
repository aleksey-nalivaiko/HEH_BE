using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
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
        private Discount _discount;

        public FavoritesServiceTests()
        {
            var userProvider = new Mock<IUserProvider>();
            var userRepository = new Mock<IUserRepository>();
            var discountRepository = new Mock<IDiscountRepository>();
            _service = new FavoritesService(userRepository.Object, discountRepository.Object,
                Mapper, userProvider.Object);

            InitTestData();
            userProvider.Setup(p => p.GetUserId()).Returns(_user.Id);

            userRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.Single()));

            userRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<User>)Data));

            userRepository.Setup(r => r.CreateAsync(It.IsAny<User>()))
                .Callback((User item) => { Data.Add(item); })
                .Returns(Task.CompletedTask);

            userRepository.Setup(f => f.UpdateAsync(It.IsAny<User>()))
                .Callback((User item) =>
                {
                    var oldItem = Data.FirstOrDefault(x => x.Id == item.Id);
                    if (oldItem != null)
                    {
                        Data.Remove(oldItem);
                        Data.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            discountRepository.Setup(s => s.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) =>
                    Task.FromResult((IEnumerable<Discount>)new List<Discount> { _discount }));
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
            var newFavorites = new FavoritesCreateUpdateDto
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
            var newFavorites = new FavoritesCreateUpdateDto
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

        [Fact]
        public async Task CanCheckIfDiscountsAreInFavorites()
        {
            Data.Add(_user);
            var result = await _service.DiscountsAreInFavorites(new List<Guid> { _discount.Id });

            Assert.True(result[_discount.Id]);
        }

        [Fact]
        public async Task CanCheckIfDiscountIsInFavorites()
        {
            Data.Add(_user);
            var result = await _service.DiscountIsInFavorites(_discount.Id);

            Assert.True(result);
        }

        private void InitTestData()
        {
            _discount = new Discount
            {
                Id = Guid.NewGuid(),
                Addresses = new List<Address>
                {
                    new Address
                    {
                        CityId = Guid.NewGuid(),
                        CountryId = Guid.NewGuid(),
                        Street = "street"
                    }
                },
                Phones = new List<Phone>
                {
                    new Phone
                    {
                        Id = Guid.NewGuid(),
                        Number = "+375441111111"
                    },
                    new Phone
                    {
                        Id = Guid.NewGuid(),
                        Number = "+375442222222"
                    },
                },
                CategoryId = Guid.NewGuid(),
                Conditions = "Conditions",
                TagsIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorId = Guid.NewGuid(),
                VendorName = "Vendor",
                PromoCode = "new promo code",
                StartDate = new DateTime(2021, 1, 20),
                EndDate = new DateTime(2021, 1, 25)
            };

            _favorites = new Favorites
            {
                DiscountId = _discount.Id,
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