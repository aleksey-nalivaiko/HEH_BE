using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidationServicesTests
{
    public class VendorValidationServiceTests
    {
        private readonly VendorValidationService _validationService;
        private readonly IMapper _mapper;
        private readonly List<Vendor> _vendorData;

        private Vendor _vendor;
        private Discount _discount;
        private Location _location;
        private City _city;
        private Address _address;
        private Address _address2;
        private Phone _phone;
        private UserDto _user;

        public VendorValidationServiceTests()
        {
            _mapper = MapperExtensions.Mapper;
            var vendorRepository = new Mock<IVendorRepository>();
            var discountRepository = new Mock<IDiscountRepository>();
            var locationRepository = new Mock<ILocationRepository>();
            var userService = new Mock<IUserService>();

            _vendorData = new List<Vendor>();
            var discountData = new List<Discount>();
            var locationData = new List<Location>();
            var userData = new List<UserDto>();

            InitializeData();

            _vendorData.Add(_vendor);
            discountData.Add(_discount);
            locationData.Add(_location);
            userData.Add(_user);

            vendorRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(_vendorData.FirstOrDefault(v => v.Id == id)));

            vendorRepository.Setup(r => r.Get())
                .Returns(() => _vendorData.AsQueryable());

            discountRepository.Setup(r => r.Get())
                .Returns(() => discountData.AsQueryable());

            locationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(locationData.FirstOrDefault(l => l.Id == id)));

            userService.Setup(s => s.GetProfileAsync())
                .Returns(Task.FromResult(userData.FirstOrDefault()));

            _validationService = new VendorValidationService(
                vendorRepository.Object, discountRepository.Object, locationRepository.Object, userService.Object);
        }

        [Fact]
        public async Task CanValidateVendorExists()
        {
            Assert.True(await _validationService.VendorExistsAsync(_vendor.Id, CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateVendorNotExists()
        {
            Assert.True(await _validationService.VendorNotExistsAsync(Guid.NewGuid(), CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateAddressesAndPhonesAreUnique()
        {
            var addressesIds = new List<int> { 1, 2, 3, 4 };
            var phonesIds = new List<int> { 1, 1, 2, 3, 4 };
            Assert.True(await Task.FromResult(_validationService.AddressesIdsAreUnique(addressesIds)));
            Assert.False(await Task.FromResult(_validationService.PhonesAreUnique(phonesIds)));
        }

        [Fact]
        public async Task CanValidateVendorNameExists()
        {
            Assert.False(await _validationService.VendorNameExists(_vendor.Name, CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateVendorNameNotExists()
        {
            Assert.True(await _validationService.VendorNameExists("Different name", CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateVendorNameChangedAndNotExists()
        {
            Assert.True(await _validationService.VendorNameChangedAndNotExists(_vendor.Id, _vendor.Name,
                CancellationToken.None));
            Assert.True(await _validationService.VendorNameChangedAndNotExists(_vendor.Id, "Different name",
                CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateAddressExists()
        {
            Assert.True(await _validationService.AddressExists(_location.Id, _city.Id, CancellationToken.None));
            Assert.False(await _validationService.AddressExists(Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateAddressesCanBeRemoved()
        {
            Assert.True(await _validationService.AddressesCanBeRemovedAsync(_vendor.Id,
                new List<AddressDto> { _mapper.Map<AddressDto>(_address) }, CancellationToken.None));
            Assert.False(await _validationService.AddressesCanBeRemovedAsync(_vendor.Id,
                new List<AddressDto> { _mapper.Map<AddressDto>(_address2) }, CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateAddressesAreFromVendor()
        {
            var discount = new DiscountShortDto { AddressesIds = new List<int> { 1 } };
            Assert.True(await Task.FromResult(_validationService.AddressesAreFromVendor(_mapper.Map<VendorDto>(_vendor),
                new List<DiscountShortDto> { discount })));

            discount.AddressesIds = new List<int> { 2 };

            Assert.False(await Task.FromResult(_validationService.AddressesAreFromVendor(_mapper.Map<VendorDto>(_vendor), new List<DiscountShortDto> { discount })));
        }

        [Fact]
        public async Task CanValidatePhonesAreFromVendor()
        {
            Assert.True(await _validationService.PhonesAreFromVendorAsync(_mapper.Map<VendorDto>(_vendor),
                    new List<DiscountShortDto> { _mapper.Map<DiscountShortDto>(_discount) }, CancellationToken.None));
            Assert.False(await _validationService.PhonesAreFromVendorAsync(new VendorDto(),
                    new List<DiscountShortDto> { _mapper.Map<DiscountShortDto>(_discount) }, CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateVendorFromLocation()
        {
            Assert.True(await _validationService.VendorFromLocationAsync(_vendor.Id, CancellationToken.None));

            var vendor = _vendor.DeepClone();
            vendor.Addresses = new List<Address>
            {
                _address2
            };
            vendor.Id = Guid.NewGuid();
            _vendorData.Add(vendor);

            _user.VendorNotifications.Add(vendor.Id);

            Assert.False(await _validationService.VendorFromLocationAsync(vendor.Id, CancellationToken.None));
        }

        private void InitializeData()
        {
            _phone = new Phone
            {
                Id = 1,
                Number = "+375292222222"
            };
            _vendor = new Vendor
            {
                Id = Guid.NewGuid(),
                Name = "Test name",
                Addresses = new List<Address>(),
                Phones = new List<Phone>()
            };
            _city = new City
            {
                Id = Guid.NewGuid(),
                Name = "Test city name"
            };

            var city2 = new City
            {
                Id = Guid.NewGuid(),
                Name = "Test city name 2"
            };

            _location = new Location
            {
                Cities = new List<City>
                {
                    _city,
                    city2
                },
                Country = "Test country",
                Id = Guid.NewGuid()
            };
            _address = new Address
            {
                Id = 1,
                CountryId = _location.Id,
                CityId = _city.Id,
                Street = "Test street"
            };
            _address2 = new Address
            {
                Id = 2,
                CityId = city2.Id,
                CountryId = _location.Id,
                Street = "New test street"
            };
            _discount = new Discount
            {
                Addresses = new List<Address>
                {
                    _address
                },
                VendorId =
                    _vendor.Id,
                PhonesIds = new List<int>
                {
                    1
                }
            };

            _user = new UserDto
            {
                Id = Guid.NewGuid(),
                CategoryNotifications = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                AllNotificationsAreOn = true,
                Email = "abc@mail.com",
                Favorites = new List<FavoritesShortDto>(),
                HotDiscountsNotificationIsOn = false,
                IsActive = true,
                Name = "Mary",
                NewDiscountNotificationIsOn = true,
                NewVendorNotificationIsOn = true,
                TagNotifications = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorNotifications = new List<Guid> { _vendor.Id },
                Role = UserRole.Employee,
                Address = _mapper.Map<AddressDto>(_address)
            };

            _vendor.Addresses.Add(_address);
            _vendor.Phones.Add(_phone);
        }
    }
}