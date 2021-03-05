using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.Host.Controllers.OData;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class DiscountControllerTests
        : BaseControllerTests<DiscountDto>
    {
        private readonly DiscountController _controller;
        private readonly List<DiscountExtendedDto> _extendedData;

        private DiscountDto _discount;
        private DiscountExtendedDto _discountExtended;

        public DiscountControllerTests()
        {
            _extendedData = new List<DiscountExtendedDto>();

            var service = new Mock<IDiscountService>();
            var validationService = new Mock<IDiscountValidationService>();
            var statisticsService = new Mock<IStatisticsService>();

            _controller = new DiscountController(service.Object, validationService.Object, statisticsService.Object);

            var searchData = Data;

            service.Setup(s => s.GetAsync(It.IsAny<string>()))
                .Callback((string param) =>
                {
                    if (param != null)
                    {
                        var lowerParam = param.ToLower();
                        searchData = Data.Where(d => d.Conditions.ToLower().Contains(lowerParam)
                                                     || d.VendorName.ToLower().Contains(lowerParam)).ToList();
                    }
                })
                .Returns(() => Task.FromResult(searchData.AsQueryable()));

            service.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(_extendedData.FirstOrDefault(d => d.Id == id)));

            validationService.Setup(v => v.DiscountExists(It.IsAny<Guid>(), default))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(Data.FirstOrDefault(d => d.Id == id) != null));
            statisticsService.Setup(s => s.IncrementViewsAmountAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.CompletedTask);

            InitTestData();
        }

        [Fact]
        public async Task CanGetAll()
        {
            Data.Add(_discount);
            var result = await _controller.GetAsync((string)default);
            Assert.Single(result);
        }

        [Fact]
        public async Task CanSearch()
        {
            Data.Add(_discount);
            var result = await _controller.GetAsync("cond");
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            _extendedData.Add(_discountExtended);
            var result = await _controller.GetAsync(_discount.Id);
            Assert.NotNull(result);
        }

        private void InitTestData()
        {
            _discount = new DiscountDto
            {
                Id = Guid.NewGuid(),
                PhonesIds = new List<int>
                {
                    1
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

            _discountExtended = new DiscountExtendedDto
            {
                Id = Guid.NewGuid(),
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        CityId = Guid.NewGuid(),
                        CountryId = Guid.NewGuid(),
                        Street = "street"
                    }
                },
                Phones = new List<PhoneDto>
                {
                    new PhoneDto
                    {
                        Id = 1,
                        Number = "+375441111111"
                    },
                    new PhoneDto
                    {
                        Id = 1,
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