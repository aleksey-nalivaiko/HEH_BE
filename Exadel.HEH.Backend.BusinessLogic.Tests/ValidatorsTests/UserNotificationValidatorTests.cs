using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.BusinessLogic.Validators;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidatorsTests
{
    public class UserNotificationValidatorTests
    {
        private readonly UserNotificationValidator _notificationValidator;
        private readonly VendorDto _vendor;
        private readonly VendorDto _vendorNotFromLocation;
        private readonly CategoryDto _category;
        private readonly TagDto _tag;

        public UserNotificationValidatorTests()
        {
            var categoryValidationExists = new Mock<ICategoryValidationService>();
            var tagValidationService = new Mock<ITagValidationService>();
            var vendorValidationService = new Mock<IVendorValidationService>();

            _notificationValidator = new UserNotificationValidator(categoryValidationExists.Object, tagValidationService.Object,
                vendorValidationService.Object);

            var user = new UserDto
            {
                Address = new AddressDto
                {
                    CountryId = Guid.NewGuid(),
                    CityId = Guid.NewGuid(),
                    Street = "Street"
                }
            };

            _vendor = new VendorDto
            {
                Id = Guid.NewGuid(),
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        CountryId = user.Address.CountryId,
                        CityId = user.Address.CityId
                    }
                }
            };

            _vendorNotFromLocation = new VendorDto
            {
                Id = Guid.NewGuid(),
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        CountryId = Guid.NewGuid(),
                        CityId = Guid.NewGuid()
                    }
                }
            };

            var vendors = new List<VendorDto>
            {
                _vendor,
                _vendorNotFromLocation
            };

            _category = new CategoryDto
            {
                Id = Guid.NewGuid()
            };
            var categories = new List<CategoryDto> { _category };

            _tag = new TagDto
            {
                Id = Guid.NewGuid()
            };
            var tags = new List<TagDto> { _tag };

            categoryValidationExists.Setup(s => s.CategoryExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(categories.Any(c => c.Id == id)));

            tagValidationService.Setup(s => s.TagExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(tags.Any(t => t.Id == id)));

            vendorValidationService.Setup(s => s.VendorExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(vendors.Any(v => v.Id == id)));

            vendorValidationService.Setup(s => s.VendorFromLocationAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                {
                    var vendor = vendors.First(v => v.Id == id);

                    var countryCities = vendor.Addresses
                        .GroupBy(a => a.CountryId)
                        .Select(g =>
                            new KeyValuePair<Guid, IEnumerable<Guid?>>(
                                g.Key, g.Select(a => a.CityId).Where(i => i.HasValue)))
                        .ToDictionary(a => a.Key, a => a.Value);

                    return Task.FromResult(countryCities.ContainsKey(user.Address.CountryId) && (!countryCities[user.Address.CountryId].Any()
                        || countryCities[user.Address.CountryId]
                            .Contains(user.Address.CityId)));
                });
        }

        [Fact]
        public void CanValidateTagsExist()
        {
            var result = _notificationValidator.TestValidate(new UserNotificationDto
            {
                CategoryNotifications = new List<Guid> { _category.Id },
                VendorNotifications = new List<Guid> { _vendor.Id },
                TagNotifications = new List<Guid> { Guid.NewGuid() }
            });
            result.ShouldHaveValidationErrorFor(n => n.TagNotifications);
        }

        [Fact]
        public void CanValidateCategoriesExist()
        {
            var result = _notificationValidator.TestValidate(new UserNotificationDto
            {
                CategoryNotifications = new List<Guid> { Guid.NewGuid() },
                VendorNotifications = new List<Guid> { _vendor.Id },
                TagNotifications = new List<Guid> { _tag.Id }
            });
            result.ShouldHaveValidationErrorFor(n => n.CategoryNotifications);
        }

        [Fact]
        public void CanValidateVendorsExist()
        {
            var result = _notificationValidator.TestValidate(new UserNotificationDto
            {
                CategoryNotifications = new List<Guid> { _category.Id },
                VendorNotifications = new List<Guid> { Guid.NewGuid() },
                TagNotifications = new List<Guid> { _tag.Id }
            });
            result.ShouldHaveValidationErrorFor(n => n.VendorNotifications);
        }

        [Fact]
        public void CanValidateVendorsFromLocation()
        {
            var result = _notificationValidator.TestValidate(new UserNotificationDto
            {
                CategoryNotifications = new List<Guid> { _category.Id },
                VendorNotifications = new List<Guid> { _vendorNotFromLocation.Id },
                TagNotifications = new List<Guid> { _tag.Id }
            });
            result.ShouldHaveValidationErrorFor(n => n.VendorNotifications);
        }
    }
}