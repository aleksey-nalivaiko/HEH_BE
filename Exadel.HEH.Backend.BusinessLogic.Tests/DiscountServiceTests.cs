using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Options;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.Extensions.Options;
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
            var searchService = new Mock<ISearchService<Discount, Discount>>();
            var historyService = new Mock<IHistoryService>();
            var statisticsService = new Mock<IStatisticsService>();
            var notificationOptions = new Mock<IOptions<NotificationOptions>>();
            var notificationManager = new Mock<INotificationService>();

            _service = new DiscountService(repository.Object, favoritesService.Object,
                vendorRepository.Object, Mapper, searchService.Object,
                historyService.Object, statisticsService.Object, notificationOptions.Object,
                notificationManager.Object);

            repository.Setup(r => r.Get())
                .Returns(() => Data.AsQueryable());

            repository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Discount>)Data));

            repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));
            InitTestData();

            repository.Setup(c => c.RemoveAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
                .Callback((Expression<Func<Discount, bool>> expression) =>
                {
                    Data.RemoveAll(d => expression.Compile()(d));
                })
                .Returns(Task.CompletedTask);

            repository.Setup(c => c.CreateManyAsync(It.IsAny<IEnumerable<Discount>>()))
                .Callback((IEnumerable<Discount> discounts) =>
                {
                    Data.AddRange(discounts);
                })
                .Returns(Task.CompletedTask);

            repository.Setup(c => c.CreateManyAsync(It.IsAny<IEnumerable<Discount>>()))
                .Callback((IEnumerable<Discount> discounts) =>
                {
                    Data.AddRange(discounts);
                })
                .Returns(Task.CompletedTask);

            favoritesService.Setup(s => s.DiscountsAreInFavorites(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> discountsIds) =>
                    Task.FromResult(discountsIds.ToDictionary(d => d, d => true)));

            favoritesService.Setup(s => s.DiscountIsInFavorites(It.IsAny<Guid>()))
                .Returns((Guid discountId) => Task.FromResult(true));

            vendorRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid vendorId) => Task.FromResult(_vendor));

            searchService.Setup(s => s.SearchAsync(default))
                .Returns(Task.FromResult((IEnumerable<Discount>)Data));

            notificationOptions.Setup(o => o.Value).Returns(new NotificationOptions
            {
                HotDiscountDaysLeft = 1,
                HotDiscountWeekendDaysLeft = 3
            });
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_discount);
            var result = await _service.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetAsync()
        {
            Data.Add(_discount);
            var result = await _service.GetAsync(default);
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Data.Add(_discount);
            var result = await _service.GetByIdAsync(_discount.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void CanGetHot()
        {
            Data.Add(_discount);
            var result = _service.GetHot();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanCreateManyAsync()
        {
            await _service.CreateManyAsync(new List<Discount> { _discount });
            var discount = Data.FirstOrDefault(x => x.Id == _discount.Id);

            Assert.NotNull(discount);
        }

        [Fact]
        public async Task CanUpdateManyAsync()
        {
            Data.Add(_discount.DeepClone());

            _discount.PromoCode = "new promo code";
            await _service.UpdateManyAsync(new List<Discount> { _discount });

            Assert.True(Data.Single(x => x.Id == _discount.Id).PromoCode.Equals("new promo code"));
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_discount);

            await _service.RemoveAsync(d => d.VendorId == _discount.VendorId);

            Assert.Empty(Data);
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
                PhonesIds = new List<int>
                {
                    _vendor.Phones.ElementAt(0).Id
                },
                Addresses = new List<Address>
                {
                    _vendor.Addresses.ElementAt(0)
                },
                CategoryId = Guid.NewGuid(),
                Conditions = "Conditions",
                TagsIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorId = _vendor.Id,
                VendorName = _vendor.Name,
                PromoCode = "new promo code",
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date
            };
        }
    }
}