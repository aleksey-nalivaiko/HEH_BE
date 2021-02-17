using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class VendorControllerTests : BaseControllerTests<VendorDto>
    {
        private readonly VendorController _controller;
        private VendorDto _testVendor;

        public VendorControllerTests()
        {
            var service = new Mock<IVendorService>();
            var validationService = new Mock<IVendorValidationService>();

            _controller = new VendorController(service.Object, validationService.Object);

            service.Setup(s => s.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<VendorShortDto>)Data));

            service.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));

            service.Setup(s => s.CreateAsync(It.IsAny<VendorDto>()))
                .Callback((VendorDto item) =>
                {
                    Data.Add(item);
                })
                .Returns(Task.CompletedTask);

            service.Setup(s => s.UpdateAsync(It.IsAny<VendorDto>()))
                .Callback((VendorDto item) =>
                {
                    var oldItem = Data.FirstOrDefault(x => x.Id == item.Id);
                    if (oldItem != null)
                    {
                        Data.Remove(oldItem);
                        Data.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            service.Setup(s => s.RemoveAsync(It.IsAny<Guid>()))
                .Callback((Guid id) => { Data.RemoveAll(d => d.Id == id); })
                .Returns(Task.CompletedTask);

            validationService.Setup(s => s.VendorExistsAsync(It.IsAny<Guid>(), default))
                .Returns(Task.FromResult(true));

            InitTestData();
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_testVendor);
            var result = await _controller.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Data.Add(_testVendor);
            var result = await _controller.GetByIdAsync(_testVendor.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _controller.CreateAsync(_testVendor);
            Assert.Single(Data);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Data.Add(_testVendor.DeepClone());
            _testVendor.Discounts = new List<DiscountShortDto>();

            await _controller.UpdateAsync(_testVendor);
            Assert.Empty(Data.Single().Discounts);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_testVendor);
            await _controller.RemoveAsync(_testVendor.Id);
            Assert.Empty(Data);
        }

        private void InitTestData()
        {
            var addresses = new List<AddressDto>
            {
                new AddressDto
                {
                    CityId = Guid.NewGuid(),
                    CountryId = Guid.NewGuid(),
                    Street = "street"
                }
            };

            var phonesIds = new List<PhoneDto>
            {
                new PhoneDto
                {
                    Id = 1,
                    Number = "+375441111111"
                },
                new PhoneDto
                {
                    Id = 2,
                    Number = "+375442222222"
                }
            };

            _testVendor = new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Vendor",
                Email = "v@gmail.com",
                Mailing = true,
                ViewsAmount = 100,
                Addresses = addresses,
                Phones = phonesIds,
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Type = LinkType.Website,
                        Url = "v.com"
                    }
                }
            };

            _testVendor.Discounts = new List<DiscountShortDto>
            {
                new DiscountShortDto
                {
                    Id = Guid.NewGuid(),
                    AddressesIds = addresses.Select(a => a.Id).ToList(),
                    PhonesIds = phonesIds.Select(p => p.Id).ToList(),
                    CategoryId = Guid.NewGuid(),
                    Conditions = "Conditions",
                    TagsIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                    VendorId = _testVendor.Id,
                    VendorName = _testVendor.Name,
                    PromoCode = "new promo code",
                    StartDate = new DateTime(2021, 1, 20),
                    EndDate = new DateTime(2021, 1, 25)
                }
            };
        }
    }
}