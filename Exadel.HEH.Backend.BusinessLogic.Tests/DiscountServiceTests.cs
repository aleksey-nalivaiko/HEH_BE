using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
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
        private Vendor _vendor;

        public DiscountServiceTests()
        {
            var repository = new Mock<IDiscountRepository>();
            var favoritesService = new Mock<IFavoritesService>();
            var vendorRepository = new Mock<IVendorRepository>();
            var discountSearchService = new Mock<ISearchService>();
            var searchService = new Mock<ISearchService>();
            var historyService = new Mock<IHistoryService>();

            _service = new DiscountService(repository.Object, favoritesService.Object,
                vendorRepository.Object, Mapper, searchService.Object, discountSearchService.Object, historyService.Object);

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

            vendorRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid vendorId) => Task.FromResult(_vendor));
        }

        [Fact]
        public async Task CanGetAsync()
        {
            Data.Add(_discount);
            var result = await _service.GetAsync(null);
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Data.Add(_discount);
            var result = await _service.GetByIdAsync(_discount.Id);
            Assert.NotNull(result);
        }

        private void InitTestData()
        {
            _vendor = new Vendor
            {
                Id = Guid.NewGuid(),
                Name = "Vendor",
                Links = new List<Link>
                {
                    new Link
                    {
                        Type = LinkType.Website,
                        Url = "v.com"
                    }
                },
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Id = 1,
                        CityId = Guid.NewGuid(),
                        CountryId = Guid.NewGuid(),
                        Street = "street"
                    }
                },
                Phones = new List<Phone>
                {
                    new Phone
                    {
                        Id = 1,
                        Number = "+375441111111"
                    }
                }
            };

            _discount = new Discount
            {
                Id = Guid.NewGuid(),
                AddressesIds = new List<int>
                {
                    _vendor.Addresses.ElementAt(0).Id
                },
                PhonesIds = new List<int>
                {
                    _vendor.Phones.ElementAt(0).Id
                },
                CategoryId = Guid.NewGuid(),
                Conditions = "Conditions",
                TagsIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorId = _vendor.Id,
                VendorName = _vendor.Name,
                PromoCode = "new promo code",
                StartDate = new DateTime(2021, 1, 20),
                EndDate = new DateTime(2021, 1, 25)
            };
        }
    }
}