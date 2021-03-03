using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.BusinessLogic.Validators;
using Exadel.HEH.Backend.DataAccess.Extensions;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidatorsTests
{
    public class VendorValidatorTests
    {
        private readonly Mock<IMethodProvider> _methodProvider;
        private readonly Mock<ITagValidationService> _tagValidationService;
        private readonly Mock<ICategoryValidationService> _categoryValidationService;
        private readonly Mock<IVendorValidationService> _vendorValidationService;
        private readonly Mock<IDiscountValidationService> _discountValidationService;

        private readonly List<VendorDto> _vendors;
        private readonly DiscountShortDto _discount;

        private VendorValidator _vendorValidator;

        public VendorValidatorTests()
        {
            _tagValidationService = new Mock<ITagValidationService>();
            _categoryValidationService = new Mock<ICategoryValidationService>();
            _vendorValidationService = new Mock<IVendorValidationService>();
            _discountValidationService = new Mock<IDiscountValidationService>();
            _methodProvider = new Mock<IMethodProvider>();

            _vendorValidator = new VendorValidator(_vendorValidationService.Object,
                _discountValidationService.Object, _categoryValidationService.Object,
                _tagValidationService.Object, _methodProvider.Object);

            _vendors = new List<VendorDto>
            {
                new VendorDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Food vendor",
                    Addresses = new List<AddressDto>
                    {
                        new AddressDto
                        {
                            CountryId = Guid.NewGuid(),
                            CityId = Guid.NewGuid(),
                            Street = "Street"
                        }
                    },
                    Phones = new List<PhoneDto>
                    {
                        new PhoneDto
                        {
                            Id = 1,
                            Number = "201-20-12"
                        }
                    }
                },
                new VendorDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Sport vendor",
                    Addresses = new List<AddressDto>
                    {
                        new AddressDto
                        {
                            CountryId = Guid.NewGuid(),
                            CityId = Guid.NewGuid(),
                            Street = "Street2"
                        }
                    }
                }
            };

            _vendorValidationService.Setup(s => s.VendorExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(_vendors.Any(c => c.Id == id)));

            _vendorValidationService.Setup(s => s.VendorNotExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(_vendors.All(c => c.Id != id)));

            _vendorValidationService.Setup(s => s.VendorNameNotExists(It.IsAny<string>(), CancellationToken.None))
                .Returns((string name, CancellationToken token) =>
                    Task.FromResult(_vendors.All(c => c.Name != name)));

            _vendorValidationService.Setup(s => s.VendorNameChangedAndNotExists(It.IsAny<Guid>(),
                    It.IsAny<string>(), CancellationToken.None))
                .Returns((Guid id, string name, CancellationToken token) =>
                {
                    var category = _vendors.First(c => c.Id == id);
                    if (category.Name == name)
                    {
                        return Task.FromResult(true);
                    }

                    return Task.FromResult(_vendors.All(c => c.Name != name));
                });

            _vendorValidationService.Setup(s => s.PhonesAreUnique(It.IsAny<IEnumerable<int>>()))
                .Returns((IEnumerable<int> ids) =>
                {
                    var list = ids.ToList();
                    return list.Count == list.Distinct().Count();
                });

            _vendorValidationService.Setup(s => s.AddressesIdsAreUnique(It.IsAny<IEnumerable<int>>()))
                .Returns((IEnumerable<int> ids) =>
                {
                    var list = ids.ToList();
                    return list.Count == list.Distinct().Count();
                });

            _vendorValidationService.Setup(s => s.StreetWithCity(It.IsAny<AddressDto>()))
                .Returns((AddressDto address) =>
                {
                    if (string.IsNullOrEmpty(address.Street))
                    {
                        return true;
                    }

                    return address.CityId.HasValue;
                });

            _vendorValidationService
                .Setup(s => s.AddressExists(It.IsAny<Guid>(), It.IsAny<Guid?>(), CancellationToken.None))
                .Returns(Task.FromResult(true));

            _vendorValidationService
                .Setup(s => s.AddressesAreUnique(It.IsAny<IEnumerable<AddressDto>>()))
                .Returns((IEnumerable<AddressDto> addresses) =>
                {
                    var addressesList = addresses.ToList();
                    return addressesList.Count == addressesList
                        .GroupBy(a => new { a.CountryId, a.CityId, a.Street })
                        .Select(g => g)
                        .Count();
                });

            _vendorValidationService
                .Setup(s => s.AddressesAreFromVendor(It.IsAny<VendorDto>(), It.IsAny<IEnumerable<DiscountShortDto>>()))
                .Returns((VendorDto vendor, IEnumerable<DiscountShortDto> vendorDiscounts) =>
                {
                    var discountAddressesIds = vendorDiscounts.SelectMany(d => d.AddressesIds)
                        .Distinct()
                        .ToList();

                    var vendorAddressesIds = vendor.Addresses.Select(p => p.Id);

                    return discountAddressesIds.All(id => vendorAddressesIds.Contains(id));
                });

            _vendorValidationService
                .Setup(s => s.PhonesAreFromVendorAsync(It.IsAny<VendorDto>(), It.IsAny<IEnumerable<DiscountShortDto>>(), CancellationToken.None))
                .Returns((VendorDto vendor, IEnumerable<DiscountShortDto> vendorDiscounts, CancellationToken token) =>
                {
                    var discountPhonesIds = vendorDiscounts.SelectMany(d =>
                        {
                            if (d.PhonesIds != null)
                            {
                                return d.PhonesIds;
                            }

                            return new List<int>();
                        })
                        .Distinct()
                        .ToList();

                    if (vendor.Phones != null)
                    {
                        var newPhonesIds = vendor.Phones.Select(a => a.Id).ToList();

                        if (vendor.Id != Guid.Empty)
                        {
                            var oldVendor = _vendors.First(v => v.Id == vendor.Id);
                            if (oldVendor != null)
                            {
                                var vendorPhonesIds = oldVendor.Phones.Select(a => a.Id);

                                var phonesToBeRemoved = vendorPhonesIds
                                    .Where(p => !newPhonesIds.Contains(p)).ToList();

                                if (phonesToBeRemoved.Any())
                                {
                                    return Task.FromResult(discountPhonesIds.All(p => newPhonesIds.Contains(p)
                                                                      || phonesToBeRemoved.Contains(p)));
                                }
                            }
                        }
                        else
                        {
                            return Task.FromResult(discountPhonesIds.All(p => newPhonesIds.Contains(p)));
                        }
                    }
                    else
                    {
                        if (discountPhonesIds.Any() && vendor.Id == Guid.Empty)
                        {
                            return Task.FromResult(false);
                        }
                    }

                    return Task.FromResult(true);
                });

            _discount = new DiscountShortDto
            {
                Id = Guid.NewGuid(),
                VendorId = _vendors[0].Id,
                VendorName = _vendors[0].Name,
                AddressesIds = new List<int>
                {
                    _vendors[0].Addresses.ElementAt(0).Id
                },
                PhonesIds = new List<int>
                {
                    _vendors[0].Phones.ElementAt(0).Id
                }
            };

            var discounts = new List<DiscountShortDto>
            {
                _discount
            };

            _discountValidationService.Setup(s => s.DiscountNotExists(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(discounts.All(d => d.Id != id)));

            _discountValidationService.Setup(s => s.EndDateLaterThanStartDate(It.IsAny<DiscountShortDto>()))
                .Returns((DiscountShortDto discount) =>
                {
                    if (discount.EndDate != null)
                    {
                        return discount.EndDate.Value.Date >= discount.StartDate.Date;
                    }

                    return true;
                });
        }

        [Fact]
        public void CanValidateDiscountEndDateLaterThanStartDate()
        {
            _discount.StartDate = DateTime.Now;
            _discount.EndDate = DateTime.MinValue;

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    }
                },
                Discounts = new List<DiscountShortDto>
                {
                    _discount
                }
            });
            result.ShouldHaveValidationErrorFor(v => v.Discounts);
        }

        [Fact]
        public void CanValidateDiscountAddressesFromVendor()
        {
            var discount = _discount.DeepClone();
            discount.AddressesIds = new List<int> { 2 };

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    }
                },
                Discounts = new List<DiscountShortDto>
                {
                    discount
                }
            });
            result.ShouldHaveValidationErrorFor(v => v.Discounts);
        }

        [Fact]
        public void CanValidateDiscountPhonesFromVendor()
        {
            var discount = _discount.DeepClone();
            discount.PhonesIds = new List<int> { 2 };

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty",
                Phones = new List<PhoneDto>
                {
                    new PhoneDto
                    {
                        Id = 1,
                        Number = "2"
                    }
                },
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    }
                },
                Discounts = new List<DiscountShortDto>
                {
                    discount
                }
            });
            result.ShouldHaveValidationErrorFor(v => v.Discounts);
        }

        [Fact]
        public void CanValidateDiscountAddressesIdsNotNull()
        {
            var discount = _discount.DeepClone();
            discount.AddressesIds = null;

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    }
                },
                Discounts = new List<DiscountShortDto>
                {
                    discount
                }
            });
            result.ShouldHaveValidationErrorFor("AddressId[0]");
        }

        [Fact]
        public void CanValidateDiscountAddressesIdsNotEmpty()
        {
            var discount = _discount.DeepClone();
            discount.AddressesIds = new List<int>();

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    }
                },
                Discounts = new List<DiscountShortDto>
                {
                    discount
                }
            });
            result.ShouldHaveValidationErrorFor("AddressId[0]");
        }

        [Fact]
        public void CanValidateDiscountAddressesIdsUnique()
        {
            var discount = _discount.DeepClone();
            discount.AddressesIds = new List<int> { 1, 1 };

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    }
                },
                Discounts = new List<DiscountShortDto>
                {
                    discount
                }
            });
            result.ShouldHaveValidationErrorFor("AddressId[0]");
        }

        [Fact]
        public void CanValidateDiscountPhonesIdsUnique()
        {
            var discount = _discount.DeepClone();
            discount.PhonesIds = new List<int> { 1, 1 };

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    }
                },
                Phones = new List<PhoneDto>
                {
                    new PhoneDto
                    {
                        Id = 1,
                        Number = "123"
                    }
                },
                Discounts = new List<DiscountShortDto>
                {
                    discount
                }
            });
            result.ShouldHaveValidationErrorFor("PhoneId[0]");
        }

        [Fact]
        public void CanValidateDiscountIdNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("POST");
            _vendorValidator = new VendorValidator(_vendorValidationService.Object,
                _discountValidationService.Object, _categoryValidationService.Object,
                _tagValidationService.Object, _methodProvider.Object);

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty",
                Discounts = new List<DiscountShortDto>
                {
                    _discount
                }
            });
            result.ShouldHaveValidationErrorFor("DiscountId[0]");
        }

        [Fact]
        public void CanValidateIdNotEmpty()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _vendorValidator = new VendorValidator(_vendorValidationService.Object,
                _discountValidationService.Object, _categoryValidationService.Object,
                _tagValidationService.Object, _methodProvider.Object);

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.Empty,
                Name = "Beauty"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateIdExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _vendorValidator = new VendorValidator(_vendorValidationService.Object,
                _discountValidationService.Object, _categoryValidationService.Object,
                _tagValidationService.Object, _methodProvider.Object);

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateIdNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("POST");
            _vendorValidator = new VendorValidator(_vendorValidationService.Object,
                _discountValidationService.Object, _categoryValidationService.Object,
                _tagValidationService.Object, _methodProvider.Object);

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateNameNotNull()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = null
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameNotEmpty()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = string.Empty
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameMaxLength()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = new string('a', 51)
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateEmailFormat()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Email = "email"
            });
            result.ShouldHaveValidationErrorFor(c => c.Email);
        }

        [Fact]
        public void CanValidatePhoneIdNotEmpty()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Phones = new List<PhoneDto>
                {
                    new PhoneDto
                    {
                        Id = 0,
                        Number = string.Empty
                    }
                }
            });
            result.ShouldHaveValidationErrorFor("PhoneId[0]");
        }

        [Fact]
        public void CanValidatePhonesUnique()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Phones = new List<PhoneDto>
                {
                    new PhoneDto
                    {
                        Id = 1
                    },
                    new PhoneDto
                    {
                        Id = 1
                    }
                }
            });
            result.ShouldHaveValidationErrorFor("PhoneId");
        }

        [Fact]
        public void CanValidatePhoneNumberNotNull()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Phones = new List<PhoneDto>
                {
                    new PhoneDto
                    {
                        Id = 1
                    }
                }
            });
            result.ShouldHaveValidationErrorFor("PhoneNumber[0]");
        }

        [Fact]
        public void CanValidateAddressesNotNull()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty"
            });
            result.ShouldHaveValidationErrorFor("Addresses");
        }

        [Fact]
        public void CanValidateAddressesNotEmpty()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Addresses = new List<AddressDto>()
            });
            result.ShouldHaveValidationErrorFor("Addresses");
        }

        [Fact]
        public void CanValidateAddressNotNull()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                   null
                }
            });
            result.ShouldHaveValidationErrorFor("Addresses[0]");
        }

        [Fact]
        public void CanValidateStreetWithCity()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        CountryId = Guid.NewGuid(),
                        Street = "Street"
                    }
                }
            });
            result.ShouldHaveValidationErrorFor("Addresses[0]");
        }

        [Fact]
        public void CanValidateAddressIdNotEmpty()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto()
                }
            });
            result.ShouldHaveValidationErrorFor("AddressId[0]");
        }

        [Fact]
        public void CanValidateAddressCountryIdNotEmpty()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1
                    }
                }
            });
            result.ShouldHaveValidationErrorFor("CountryId[0]");
        }

        [Fact]
        public void CanValidateCityMaxLength()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid(),
                        CityId = Guid.NewGuid(),
                        Street = new string('a', 51)
                    }
                }
            });
            result.ShouldHaveValidationErrorFor("Street[0]");
        }

        [Fact]
        public void CanValidateAddressesIdsUnique()
        {
            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    },
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = Guid.NewGuid()
                    }
                }
            });
            result.ShouldHaveValidationErrorFor("AddressId");
        }

        [Fact]
        public void CanValidateAddressesUnique()
        {
            var countryId = Guid.NewGuid();

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[0].Id,
                Name = "Beauty",
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CountryId = countryId
                    },
                    new AddressDto
                    {
                        Id = 2,
                        CountryId = countryId
                    }
                }
            });
            result.ShouldHaveValidationErrorFor("Addresses");
        }

        [Fact]
        public void CanValidateNameNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("POST");
            _vendorValidator = new VendorValidator(_vendorValidationService.Object,
                _discountValidationService.Object, _categoryValidationService.Object,
                _tagValidationService.Object, _methodProvider.Object);

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Food vendor"
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameChangedAndNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _vendorValidator = new VendorValidator(_vendorValidationService.Object,
                _discountValidationService.Object, _categoryValidationService.Object,
                _tagValidationService.Object, _methodProvider.Object);

            var result = _vendorValidator.TestValidate(new VendorDto
            {
                Id = _vendors[1].Id,
                Name = "Food vendor"
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
    }
}