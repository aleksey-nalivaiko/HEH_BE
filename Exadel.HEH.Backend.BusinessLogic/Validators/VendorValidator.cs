using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class VendorValidator : AbstractValidator<VendorDto>
    {
        public VendorValidator(IVendorValidationService vendorValidationService, IMethodProvider methodProvider)
        {
            var methodType = methodProvider.GetMethodUpperName();

            RuleFor(v => v.Id).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .MustAsync(vendorValidationService.VendorExists)
                .When(dto => methodType == "PUT", ApplyConditionTo.CurrentValidator)
                .WithMessage("Vendor with this id doesn't exists.")
                .MustAsync(vendorValidationService.VendorNotExists)
                .When(dto => methodType == "POST", ApplyConditionTo.CurrentValidator)
                .WithMessage("Vendor already exists.");
        }
    }
}