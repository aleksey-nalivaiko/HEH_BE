using System;
using System.Collections.Generic;
using System.Text;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class DiscountValidator : AbstractValidator<DiscountDto>
    {
        public DiscountValidator(IDiscountValidationService validationService,
            ICategoryValidationService categoryValidationService,
            ITagValidationService tagValidationService,
            IMethodProvider methodProvider)
        {
            CascadeMode = CascadeMode.Stop;

            var methodType = methodProvider.GetMethodUpperName();

            RuleFor(r => r.Id)
                .NotNull()
                .NotEmpty()
                .MustAsync(validationService.DiscountExists)
                .WithMessage("Discount with this id doesn't exists.")
                .When(dto => methodType == "PUT");

            RuleFor(r => r.Id)
                .MustAsync(validationService.DiscountNotExists)
                .WithMessage("Discount with this id already exists.")
                .When(dto => methodType == "POST");

            RuleFor(r => r.Conditions)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255)
                .WithMessage("Discount condition can't be more then 255 simbols and can't be empty");

            RuleFor(r => r.PromoCode)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Discount promoCode can't be more then 50 simbols and can't be empty");

            RuleFor(r => r.VendorName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Discount vendorName can't be more then 50 simbols and can't be empty");

            RuleFor(v => v.Addresses)
                .MustAsync(async (discount, addresses, cancellation) =>
                    await validationService.AddressesAreUniqueAsync(discount.Id, addresses, cancellation))
                .WithMessage("There are addresses with same id.");

            RuleFor(r => r.CategoryId)
                .MustAsync(categoryValidationService.CategoryExistsAsync)
                .WithMessage("Some of provided categories don't exist");

            RuleFor(r => r.TagsIds)
                .MustAsync(tagValidationService.TagsExistsAsync)
                .WithMessage("Some of provided tags don't exist");
        }
    }
}
