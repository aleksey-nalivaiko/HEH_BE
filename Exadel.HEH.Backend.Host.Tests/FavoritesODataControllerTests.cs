using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers.OData;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class FavoritesODataControllerTests : BaseControllerTests<FavoritesDto>
    {
        private readonly FavoritesController _controller;

        private readonly FavoritesDto _favorites;

        public FavoritesODataControllerTests()
        {
            var favoritesService = new Mock<IFavoritesService>();

            _controller = new FavoritesController(favoritesService.Object);

            favoritesService.Setup(s => s.GetAsync(default))
                .Returns(Task.FromResult(Data.AsQueryable()));

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
        }

        [Fact]
        public async Task CanGetAsync()
        {
            Data.Add(_favorites);
            var result = await _controller.GetAsync(default);
            Assert.Single(result);
        }
    }
}