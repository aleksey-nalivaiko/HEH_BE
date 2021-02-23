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

                RuleForEach(v => v.Addresses)
                    .MustAsync(async (adresses, cancellation) =>
                        await discountValidationService.AddressesExist(adresses.CountryId, adresses.CityId,
                            cancellation))
                    .WithMessage("Provided combination of country/city doesn't exist");

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
                    .WithName("DiscountId")
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
                    .WithName("DiscountId")
                    .When(v => v.Discounts != null);

                RuleForEach(v => v.Addresses)
                    .MustAsync(async (adresses, cancellation) =>
                        await discountValidationService.AddressesExist(adresses.CountryId, adresses.CityId,
                            cancellation))
                    .WithMessage("Provided combination of country/city doesn't exist");
            });

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .MustAsync(vendorValidationService.VendorNameExists)
                .WithName("VendorName")
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
                .WithName("AddressId");

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
                .WithName("PhoneId");

            RuleFor(v => v.Discounts)
                .Must(vendorValidationService.AddressesAreFromVendor)
                .WithMessage("Vendor does not contain addresses defined in discounts.")
                .MustAsync(vendorValidationService.PhonesAreFromVendorAsync)
                .WithMessage("Vendor does not contain phones defined in discounts.")
                .When(v => v.Discounts != null);

            RuleForEach(v => v.Discounts.Select(d => d.Conditions))
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("Discount condition can't be more than 255 symbols")
                .WithName("Conditions");

            RuleForEach(v => v.Discounts.Select(d => d.PromoCode))
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Discount promoCode can't be more than 50 symbols")
                .WithName("Promocode");

            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("VendorName can't be more than 50 symbols")
                .WithName("VendorName");

            RuleForEach(v => v.Discounts.Select(d => d.CategoryId))
                .MustAsync(categoryValidationService.CategoryExistsAsync)
                .WithMessage("Some of provided categories don't exist")
                .WithName("CategoryId");

            RuleForEach(v => v.Discounts.Select(d => d.TagsIds))
                .MustAsync(tagValidationService.TagsExistsAsync)
                .WithMessage("Some of provided tags don't exist")
                .WithName("TagsIds");
        }
    }
}