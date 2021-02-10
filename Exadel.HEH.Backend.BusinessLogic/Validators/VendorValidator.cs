using System;
using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class VendorValidator : AbstractValidator<VendorDto>
    {
        public VendorValidator(IVendorValidationService vendorValidationService,
            IDiscountValidationService discountValidationService,
            IMethodProvider methodProvider)
        {
            CascadeMode = CascadeMode.Stop;

            var methodType = methodProvider.GetMethodUpperName();

            When(dto => methodType == "PUT", () =>
            {
                RuleFor(v => v.Id)
                    .NotNull()
                    .NotEmpty()
                    .MustAsync(vendorValidationService.VendorExistsAsync)
                    .WithMessage("Vendor with this id doesn't exists.");

                RuleFor(v => v.Addresses)
                    .MustAsync(async (vendor, addresses, cancellation) =>
                        await vendorValidationService.AddressesCanBeRemovedAsync(vendor.Id, addresses, cancellation))
                    .WithMessage("Address(es) cannot be removed: they are used in discount(s).");

                RuleFor(v => v.Addresses)
                    .MustAsync(async (vendor, addresses, cancellation) =>
                        await vendorValidationService.AddressesAreUniqueAsync(vendor.Id, addresses, cancellation))
                    .WithMessage("There are addresses with same id.");

                RuleFor(v => v.Discounts)
                    .MustAsync(async (vendor, discounts, cancellation) =>
                        await vendorValidationService.AddressesAreFromVendorAsync(vendor.Id, discounts, cancellation))
                    .WithMessage("Vendor does not contain addresses defined in discounts.")
                    .MustAsync(async (vendor, discounts, cancellation) =>
                        await vendorValidationService.PhonesAreFromVendorAsync(vendor.Id, discounts, cancellation))
                    .WithMessage("Vendor does not contain phones defined in discounts.");

                RuleFor(v => v.Phones)
                    .MustAsync(async (vendor, phones, cancellation) =>
                        await vendorValidationService.PhonesAreUniqueAsync(vendor.Id, phones, cancellation))
                    .WithMessage("There are phones with same id.");
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
            });
        }
    }
}