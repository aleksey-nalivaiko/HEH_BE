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

            When(dto => methodType == "PUT", () =>
            {
                RuleFor(v => v.Id)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty()
                    .MustAsync(vendorValidationService.VendorExistsAsync)
                    .WithMessage("Vendor with this id doesn't exists.");

                RuleFor(v => v.Addresses)
                    .MustAsync(async (vendor, addresses, cancellation) =>
                        await vendorValidationService.AddressesCanBeRemovedAsync(vendor.Id, addresses, cancellation))
                    .WithMessage("Address(es) cannot be removed: they are used in discount(s).")
                    .When(v => v.Addresses != null);

                RuleForEach(v => v.Discounts.Select(d => d.Id))
                    .NotNull()
                    .NotEmpty()
                    .MustAsync(discountValidationService.DiscountExists)
                    .WithMessage("Discount with this id doesn't exists.")
                    .WithName("Discounts")
                    .When(v => v.Discounts != null);
            });

            When(dto => methodType == "POST", () =>
            {
                RuleFor(v => v.Id)
                    .MustAsync(vendorValidationService.VendorNotExistsAsync)
                    .WithMessage("Vendor with this id already exists.");

                RuleForEach(v => v.Discounts.Select(d => d.Id))
                    .MustAsync(discountValidationService.DiscountNotExists)
                    .WithMessage("Discount with this id already exists.")
                    .WithName("Discounts")
                    .When(v => v.Discounts != null);
            });

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50)
                .MustAsync(vendorValidationService.VendorNameExists)
                .WithMessage("Such vendor name already exists");

            RuleFor(v => v.Addresses.Select(a => a.Id))
                .Must(vendorValidationService.AddressesAreUnique)
                .WithMessage("There are addresses with same id.")
                .When(v => v.Addresses != null)
                .WithName("Addresses");

            RuleForEach(v => v.Addresses.Select(a => a.Id))
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .When(v => v.Addresses != null)
                .WithName("Addresses");

            RuleFor(v => v.Phones.Select(p => p.Id))
                .Must(vendorValidationService.PhonesAreUnique)
                .WithMessage("There are phones with same id.")
                .When(v => v.Phones != null)
                .WithName("Phones");

            RuleForEach(v => v.Phones.Select(p => p.Id))
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .When(v => v.Phones != null)
                .WithName("Phones");

            RuleFor(v => v.Discounts)
                .Must(vendorValidationService.AddressesAreFromVendor)
                .WithMessage("Vendor does not contain addresses defined in discounts.")
                .MustAsync(vendorValidationService.PhonesAreFromVendorAsync)
                .WithMessage("Vendor does not contain phones defined in discounts.")
                .When(v => v.Discounts != null);

            RuleForEach(v => v.Discounts.Select(d => d.Conditions))
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .WithName("Conditions")
                .When(v => v.Discounts != null);

            RuleForEach(v => v.Discounts.Select(d => d.PromoCode))
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithName("Promocode")
                .When(v => v.Discounts != null);

            RuleForEach(v => v.Discounts.Select(d => d.CategoryId))
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MustAsync(categoryValidationService.CategoryExistsAsync)
                .WithMessage("Category doesn't exist")
                .WithName("Category")
                .When(v => v.Discounts != null);

            RuleForEach(v => v.Discounts.Select(d => d.TagsIds))
                .MustAsync(tagValidationService.TagsExistsAsync)
                .WithMessage("Some of provided tags don't exist")
                .WithName("Tags")
                .When(v => v.Discounts != null);

            RuleForEach(v => v.Addresses)
                .MustAsync(async (addresses, cancellation) =>
                    await discountValidationService.AddressesExist(addresses.CountryId, addresses.CityId,
                        cancellation))
                .WithMessage("Provided combination of country/city doesn't exist")
                .When(v => v.Addresses != null);
        }
    }
}