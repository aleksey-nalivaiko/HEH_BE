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
        }
    }
}