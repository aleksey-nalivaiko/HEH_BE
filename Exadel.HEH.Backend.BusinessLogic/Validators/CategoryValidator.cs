using System;
using System.Collections.Generic;
using System.Text;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator(ICategoryValidationService categoryValidationService)
        {
            RuleFor(r => r.Id).NotNull().NotEmpty()
                .MustAsync(categoryValidationService.DiscountContainsCategory).WithMessage("Discount do not contain category");
            RuleFor(r => r.Name).MaximumLength(50).NotEmpty().NotNull()
                .WithMessage("Name should be less the 50 simbols and not empty");
        }
    }
}
