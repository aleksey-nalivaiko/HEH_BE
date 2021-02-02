using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class DiscountControllerTests
        : BaseControllerTests<DiscountDto>
    {
        private readonly DiscountController _controller;
        private DiscountDto _discount;

        public DiscountControllerTests()
        {
            var service = new Mock<IDiscountService>();
            _controller = new DiscountController(service.Object);

            var data = Data;

            service.Setup(s => s.Get(It.IsAny<string>()))
                .Callback((string param) =>
                {
                    if (param != null)
                    {
                        var lowerParam = param.ToLower();
                        data = Data.Where(d => d.Conditions.ToLower().Contains(lowerParam)
                                               || d.VendorName.ToLower().Contains(lowerParam)).ToList();
                    }
                })
                .Returns(() => data.AsQueryable());
            service.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(d => d.Id == id)));

            InitTestData();
        }

        [Fact]
        public void CanGetAll()
        {
            Data.Add(_discount);
            var result = _controller.Get(default(string));
            Assert.Single(result);
        }

        [Fact]
        public void CanSearch()
        {
            Data.Add(_discount);
            var result = _controller.Get("cond");
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Data.Add(_discount);
            var result = await _controller.Get(_discount.Id);
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task CanUpdate()
        //{
        //    Data.Add(_discount);
        //    _userDto.IsActive = false;

        //    await _controller.UpdateAsync(_userDto);
        //    Assert.False(Data.Single(x => x.Id == _discount.Id).IsActive);
        //}

        private void InitTestData()
        {
            _discount = new DiscountDto
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
                        Id = Guid.NewGuid(),
                        Number = "+375441111111"
                    },
                    new PhoneDto
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