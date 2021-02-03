using System;
using System.Collections.Generic;
using System.Text;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class TagValidation : AbstractValidator<TagDto>
    {
        public TagValidation()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty()
                .WithMessage("TagId should be not empty");
            RuleFor(r => r.CategoryId).NotNull().NotEmpty()
                .WithMessage("Category should be not empty");
            RuleFor(r => r.Name).NotNull().NotEmpty().MaximumLength(50)
                .WithMessage("Name should be not empty and can not contain more then 50 simbols");
        }
    }
}
