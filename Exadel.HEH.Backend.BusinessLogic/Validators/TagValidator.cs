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
        public TagValidator(ITagValidationService tagValidationService,
            IMethodProvider methodProvider)
        {
            var methodType = methodProvider.GetMethodUpperName();

            RuleFor(r => r.Id).NotNull().NotEmpty().WithMessage("Id should not be null or empty")
                .When(dto => methodType == "PUT");
            RuleFor(r => r.Name).MaximumLength(50).NotEmpty().NotNull()
                .WithMessage("Name should be less the 50 simbols and not empty");
            RuleFor(r => r.CategoryId).NotNull().NotEmpty().WithMessage("Category should not be null or empty");
        }
    }
}
