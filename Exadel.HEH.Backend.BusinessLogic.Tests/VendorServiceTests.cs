using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class VendorServiceTests : BaseServiceTests<Vendor>
    {
        private readonly VendorService _service;
        private readonly List<Discount> _discountsData;
        private readonly IMapper _mapper;

        private Vendor _testVendor;
        private Discount _testDiscount;

        public VendorServiceTests()
        {
            var vendorRepository = new Mock<IVendorRepository>();
            var discountRepository = new Mock<IDiscountRepository>();
            _mapper = MapperExtensions.Mapper;

            _service = new VendorService(vendorRepository.Object, discountRepository.Object, _mapper);

            _discountsData = new List<Discount>();

            discountRepository.Setup(r => r.Get())
                .Returns(_discountsData.AsQueryable());

            discountRepository.Setup(r => r.CreateManyAsync(It.IsAny<IEnumerable<Discount>>()))
                .Callback((IEnumerable<Discount> items) =>
                {
                    _discountsData.AddRange(items);
                })
                .Returns(Task.CompletedTask);

            discountRepository.Setup(r => r.UpdateManyAsync(It.IsAny<IEnumerable<Discount>>()))
                .Callback((IEnumerable<Discount> items) =>
                {
                    _discountsData.Clear();
                    _discountsData.AddRange(items);
                })
                .Returns(Task.CompletedTask);

            discountRepository.Setup(r => r.RemoveAsync(It.IsAny<Expression<Func<Discount, bool>>>()))
                .Callback((Expression<Func<Discount, bool>> expression) =>
                {
                    _discountsData.RemoveAll(d => expression.Compile()(d));
                })
                .Returns(Task.CompletedTask);

            vendorRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Vendor>)Data));

            vendorRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));

            vendorRepository.Setup(r => r.CreateAsync(It.IsAny<Vendor>()))
                .Callback((Vendor item) => { Data.Add(item); })
                .Returns(Task.CompletedTask);

            vendorRepository.Setup(f => f.UpdateAsync(It.IsAny<Vendor>()))
                .Callback((Vendor item) =>
                {
                    var oldItem = Data.FirstOrDefault(x => x.Id == item.Id);
                    if (oldItem != null)
                    {
                        Data.Remove(oldItem);
                        Data.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            vendorRepository.Setup(r => r.RemoveAsync(It.IsAny<Guid>()))
                .Callback((Guid id) => { Data.RemoveAll(d => d.Id == id); })
                .Returns(Task.CompletedTask);

            InitTestData();
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_testVendor);
            _discountsData.Add(_testDiscount);

            var result = (await _service.GetAllAsync()).ToList();

            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetAllDetailedAsync()
        {
            Data.Add(_testVendor);
            _discountsData.Add(_testDiscount);

            var result = (await _service.GetAllDetailedAsync()).ToList();

            Assert.Single(result);
            Assert.Single(result.Single().Discounts);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Data.Add(_testVendor);
            _discountsData.Add(_testDiscount);

            var result = await _service.GetByIdAsync(_testVendor.Id);

            Assert.NotNull(result);
            Assert.Single(result.Discounts);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            var vendor = _mapper.Map<VendorDto>(_testVendor);
            vendor.Discounts = _mapper.Map<IEnumerable<DiscountDto>>(new List<Discount> { _testDiscount });

            await _service.CreateAsync(vendor);

            Assert.Single(Data);
            Assert.Single(_discountsData);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Data.Add(_testVendor.DeepClone());

            _testVendor.Name = "New name";
            var vendor = _mapper.Map<VendorDto>(_testVendor);
            vendor.Discounts = _mapper.Map<IEnumerable<DiscountDto>>(new List<Discount> { _testDiscount });

            await _service.UpdateAsync(vendor);

            Assert.Equal(_testVendor.Name, Data.Single().Name);
            Assert.Single(_discountsData);
        }

        [Fact]
        public async Task CanRemoveDiscountAsync()
        {
            Data.Add(_testVendor.DeepClone());

            var newDiscount = _testDiscount.DeepClone();
            newDiscount.Id = Guid.NewGuid();

            var vendor = _mapper.Map<VendorDto>(_testVendor);
            vendor.Discounts = _mapper.Map<IEnumerable<DiscountDto>>(new List<Discount> { newDiscount });

            await _service.UpdateAsync(vendor);

            Assert.Equal(_discountsData.Single().Id, newDiscount.Id);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_testVendor);

            await _service.RemoveAsync(_testVendor.Id);

            Assert.Empty(Data);
        }

        private void InitTestData()
        {
            var addresses = new List<Address>
            {
                new Address
                {
                    CityId = Guid.NewGuid(),
                    CountryId = Guid.NewGuid(),
                    Street = "street"
                }
            };

            var phonesIds = new List<Phone>
            {
                new Phone
                {
                    Id = 1,
                    Number = "+375441111111"
                },
                new Phone
                {
                    Id = 2,
                    Number = "+375442222222"
                }
            };

            _testVendor = new Vendor
            {
                Id = Guid.NewGuid(),
                Name = "Vendor",
                Email = "v@gmail.com",
                Mailing = true,
                ViewsAmount = 100,
                Addresses = addresses,
                Phones = phonesIds,
                Links = new List<Link>
                {
                    new Link
                    {
                        Type = LinkType.Website,
                        Url = "v.com"
                    }
                }
            };

            _testDiscount = new Discount
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
            };
        }
    }
}