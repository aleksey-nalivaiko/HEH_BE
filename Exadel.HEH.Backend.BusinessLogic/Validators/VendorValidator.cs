using System;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class VendorValidator : AbstractValidator<VendorDto>
    {
        public VendorValidator(IVendorValidationService vendorValidationService,
            IMethodProvider methodProvider)
        {
            var methodType = methodProvider.GetMethodUpperName();

            RuleFor(v => v.Id).Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MustAsync(vendorValidationService.VendorExists)
                .WithMessage("Vendor with this id doesn't exists.")
                .When(dto => methodType == "PUT");

            RuleFor(v => v.Id)
                .MustAsync(vendorValidationService.VendorNotExists)
                .WithMessage("Vendor with this id already exists.")
                .When(dto => methodType == "POST" && dto.Id != Guid.Empty);
        }
    }
}