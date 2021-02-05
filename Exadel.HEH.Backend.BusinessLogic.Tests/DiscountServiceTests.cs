using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class DiscountServiceTests : BaseServiceTests<Discount>
    {
        private readonly DiscountService _service;
        private Discount _discount;

        public DiscountServiceTests()
        {
            var repository = new Mock<IDiscountRepository>();
            var favoritesService = new Mock<IFavoritesService>();

            _service = new DiscountService(repository.Object, favoritesService.Object, Mapper);
            repository.Setup(r => r.Get())
                .Returns(() => Data.AsQueryable());
            repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));
            InitTestData();

            favoritesService.Setup(s => s.DiscountsAreInFavorites(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> discountsIds) =>
                    Task.FromResult(discountsIds.ToDictionary(d => d, d => true)));

            favoritesService.Setup(s => s.DiscountIsInFavorites(It.IsAny<Guid>()))
                .Returns((Guid discountId) => Task.FromResult(true));
        }

        [Fact]
        public async Task CanGetAsync()
        {
            Data.Add(_discount);
            var result = await _service.GetAsync(null);
            Assert.Single(result);
        }

        [Fact]
        public async Task CanSearchAsync()
        {
            Data.Add(_discount);
            var result = await _service.GetAsync("cond");
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Data.Add(_discount);
            var result = await _service.GetByIdAsync(_discount.Id);
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task CanUpdate()
        //{
        //    Data.Add(_discount.DeepClone());
        //    _discount.IsActive = false;

        //    await _service.UpdateAsync(_discount);
        //    Assert.False(Data.Single(x => x.Id == _discount.Id).IsActive);
        //}

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
                    }
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
        }
    }
}