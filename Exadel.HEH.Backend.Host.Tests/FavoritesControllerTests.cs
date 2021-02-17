using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.Host.Controllers;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class FavoritesControllerTests
    {
        private readonly FavoritesController _controller;
        private readonly List<FavoritesDto> _data;
        private readonly List<FavoritesShortDto> _dataCreateUpdate;
        private FavoritesDto _favorites;
        private FavoritesShortDto _favoritesShort;

        public FavoritesControllerTests()
        {
            var service = new Mock<IFavoritesService>();
            var validationService = new Mock<IFavoritesValidationService>();

            _controller = new FavoritesController(service.Object, validationService.Object);
            _data = new List<FavoritesDto>();
            _dataCreateUpdate = new List<FavoritesShortDto>();
            InitTestData();

            service.Setup(s => s.GetAllAsync()).Returns(Task.FromResult((IEnumerable<FavoritesDto>)_data));

            service.Setup(s => s.CreateAsync(It.IsAny<FavoritesShortDto>()))
                .Callback((FavoritesShortDto item) =>
                {
                    _dataCreateUpdate.Add(item);
                })
                .Returns(Task.CompletedTask);

            service.Setup(s => s.UpdateAsync(It.IsAny<FavoritesShortDto>()))
                .Callback((FavoritesShortDto item) =>
                {
                    var oldItem = _dataCreateUpdate.Single();
                    if (oldItem != null)
                    {
                        _dataCreateUpdate.Remove(oldItem);
                        _dataCreateUpdate.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            service.Setup(s => s.RemoveAsync(It.IsAny<Guid>()))
                .Callback((Guid id) => { _data.RemoveAll(d => d.Id == id); })
                .Returns(Task.CompletedTask);

            validationService.Setup(v => v.DiscountExists(It.IsAny<Guid>(), default))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(_data.FirstOrDefault(d => d.Id == id) != null));

            validationService.Setup(v => v.UserFavoritesNotExists(It.IsAny<Guid>(), default))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(_data.FirstOrDefault(d => d.Id == id) != null));
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            _data.Add(_favorites);
            _dataCreateUpdate.Add(_favoritesShort);
            var result = await _controller.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _controller.CreateAsync(_favoritesShort);
            Assert.Single(_dataCreateUpdate);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            _dataCreateUpdate.Add(_favoritesShort);
            var newFavorites = new FavoritesShortDto
            {
                DiscountId = _favorites.Id,
                Note = "New note"
            };
            await _controller.UpdateAsync(newFavorites);
            Assert.NotEqual(_dataCreateUpdate.Single().Note, _favoritesShort.Note);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            _data.Add(_favorites);
            await _controller.RemoveAsync(_favorites.Id);
            Assert.Empty(_data);
        }

        private void InitTestData()
        {
            _favorites = new FavoritesDto
            {
                Id = Guid.NewGuid(),
                Note = "Note1",
                Addresses = new List<AddressDto>(),
                PhonesIds = new List<int>
                {
                    1
                },
                CategoryId = Guid.NewGuid(),
                Conditions = "Conditions",
                TagsIds = new List<Guid> { Guid.NewGuid() },
                VendorId = Guid.NewGuid(),
                VendorName = "Vendor",
                PromoCode = "new promo code",
                StartDate = new DateTime(2021, 1, 20),
                EndDate = new DateTime(2021, 1, 25)
            };

            _favoritesShort = new FavoritesShortDto
            {
                DiscountId = Guid.NewGuid(),
                Note = "Note1",
            };
        }
    }
}