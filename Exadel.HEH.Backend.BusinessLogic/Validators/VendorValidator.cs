using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class VendorValidator : AbstractValidator<VendorDto>
    {
        public VendorValidator(IVendorValidationService vendorValidationService,
            IDiscountValidationService discountValidationService,
            ICategoryValidationService categoryValidationService,
            ITagValidationService tagValidationService,
            IMethodProvider methodProvider)
        {
            var methodType = methodProvider.GetMethodUpperName();

            CascadeMode = CascadeMode.Stop;

            When(dto => methodType == "PUT", () =>
            {
                RuleFor(v => v.Id)
                    .NotNull()
                    .NotEmpty()
                    .MustAsync(vendorValidationService.VendorExistsAsync)
                    .WithMessage("Vendor with this id doesn't exists.");

                RuleFor(v => v.Name)
                    .MustAsync(async (vendor, name, cancellation) =>
                        await vendorValidationService.VendorNameChangedAndNotExists(vendor.Id, name, cancellation))
                    .WithMessage("Such vendor name already exists.");
            });

            When(dto => methodType == "POST", () =>
            {
                RuleFor(v => v.Id)
                    .MustAsync(vendorValidationService.VendorNotExistsAsync)
                    .WithMessage("Vendor with this id already exists.");

                RuleFor(v => v.Name)
                    .MustAsync(vendorValidationService.VendorNameNotExists)
                    .WithMessage("Such vendor name already exists");

                RuleForEach(v => v.Discounts.Select(d => d.Id))
                    .MustAsync(discountValidationService.DiscountNotExists)
                    .WithMessage("Discount with this id already exists.")
                    .WithName("DiscountId")
                    .When(v => v.Discounts != null);
            });

            RuleFor(v => v.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(v => v.Email)
                .EmailAddress()
                .When(v => !string.IsNullOrEmpty(v.Email));

            RuleFor(v => v.WorkingHours)
                .Matches("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]-(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
                .When(v => !string.IsNullOrEmpty(v.WorkingHours));

            RuleForEach(v => v.Phones.Select(p => p.Id))
                .NotEmpty()
                .NotNull()
                .When(v => v.Phones != null)
                .WithName("PhoneId");

            RuleFor(v => v.Phones.Select(p => p.Id))
                .Must(vendorValidationService.PhonesAreUnique)
                .WithMessage("There are phones with same id.")
                .When(v => v.Phones != null)
                .WithName("PhoneId");

            RuleForEach(v => v.Phones.Select(p => p.Number))
                .NotEmpty()
                .NotNull()
                .When(v => v.Phones != null)
                .WithName("PhoneNumber");

            RuleFor(v => v.Addresses)
                .NotNull()
                .NotEmpty();

            RuleForEach(v => v.Addresses)
                .NotNull()
                .NotEmpty()
                .Must(vendorValidationService.StreetWithCity)
                .WithMessage("Street cannot be set without a city.");

            RuleForEach(v => v.Addresses.Select(a => a.Id))
                .NotEmpty()
                .NotNull()
                .WithName("AddressId");

            RuleFor(v => v.Addresses.Select(a => a.Id))
                .Must(vendorValidationService.AddressesIdsAreUnique)
                .WithMessage("There are addresses with same id.")
                .WithName("AddressId");

            RuleForEach(v => v.Addresses.Select(a => a.CountryId))
                .NotEmpty()
                .NotNull()
                .WithName("CountryId");

            RuleForEach(v => v.Addresses.Select(a => a.Street))
                .MaximumLength(50)
                .WithName("Street");

            RuleForEach(v => v.Addresses)
                .MustAsync(async (address, cancellation) =>
                    await vendorValidationService.AddressExists(address.CountryId, address.CityId,
                        cancellation))
                .WithMessage("Provided combination of country/city doesn't exist.");

            RuleFor(v => v.Addresses)
                .Must(vendorValidationService.AddressesAreUnique)
                .WithMessage("There are same addresses.")
                .WithName("Address");

            RuleFor(v => v.Addresses)
                .MustAsync(async (vendor, addresses, cancellation) =>
                    await vendorValidationService.AddressesCanBeRemovedAsync(vendor, cancellation))
                .WithMessage("Address(es) cannot be removed: they are used in discount(s).")
                .When(dto => methodType == "PUT");

            RuleForEach(v => v.Discounts.Select(d => d.AddressesIds))
                .NotNull()
                .NotEmpty()
                .Must((vendor, addresses) => vendorValidationService.AddressesIdsAreUnique(addresses))
                .WithMessage("There are addresses with same id.")
                .WithName("AddressId")
                .When(v => v.Discounts != null && v.Discounts.Any());

            RuleForEach(v => v.Discounts.Select(d => d.PhonesIds))
                .Must((vendor, phones) => vendorValidationService.PhonesAreUnique(phones))
                .WithMessage("There are phones with same id.")
                .WithName("PhoneId")
                .When(v => v.Discounts != null && v.Discounts.Any());

            RuleFor(v => v.Discounts)
                .Must(vendorValidationService.AddressesAreFromVendor)
                .WithMessage("Vendor does not contain addresses defined in discounts.")
                .MustAsync(vendorValidationService.PhonesAreFromVendorAsync)
                .WithMessage("Vendor does not contain phones defined in discounts.")
                .When(v => v.Discounts != null && v.Discounts.Any());

            RuleForEach(v => v.Discounts)
                .Must(discountValidationService.EndDateLaterThanStartDate)
                .WithMessage("End date must be equal or greater than start date.")
                .When(v => v.Discounts != null && v.Discounts.Any());

            RuleForEach(v => v.Discounts.Select(d => d.Conditions))
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .WithName("Conditions")
                .When(v => v.Discounts != null && v.Discounts.Any());

            RuleForEach(v => v.Discounts.Select(d => d.PromoCode))
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Promocode")
                .When(v => v.Discounts != null && v.Discounts.Any());

            RuleForEach(v => v.Discounts.Select(d => d.CategoryId))
                .NotNull()
                .NotEmpty()
                .MustAsync(categoryValidationService.CategoryExistsAsync)
                .WithMessage("Category doesn't exist.")
                .WithName("Category")
                .When(v => v.Discounts != null && v.Discounts.Any());

            RuleForEach(v => v.Discounts.Select(d => d.TagsIds))
                .MustAsync(tagValidationService.TagsExistsAsync)
                .WithMessage("Some of provided tags don't exist.")
                .WithName("Tags")
                .When(v => v.Discounts != null && v.Discounts.Any());
        }
    }
}