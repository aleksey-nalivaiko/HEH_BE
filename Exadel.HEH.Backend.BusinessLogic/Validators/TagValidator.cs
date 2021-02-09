using System;
using System.Collections.Generic;
using System.Text;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class TagValidator : AbstractValidator<TagDto>
    {
        public TagValidator(ITagValidationService tagValidationService)
        {
            RuleFor(r => r.Id).NotNull().NotEmpty().WithMessage("Id should not be null or empty");
            RuleFor(r => r.Name).MaximumLength(50).NotEmpty().NotNull()
                .WithMessage("Name should be less the 50 simbols and not empty");
            RuleFor(r => r.CategoryId).NotNull().NotEmpty().WithMessage("Category should not be null or empty");
        }
    }
}
