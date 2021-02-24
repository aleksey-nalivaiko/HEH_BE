using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class TagValidator : AbstractValidator<TagDto>
    {
        public TagValidator(ITagValidationService tagValidationService,
            ICategoryValidationService categoryValidationService,
            IMethodProvider methodProvider)
        {
            CascadeMode = CascadeMode.Stop;

            var methodType = methodProvider.GetMethodUpperName();

            RuleFor(r => r.Id)
                .NotNull()
                .NotEmpty()
                .MustAsync(tagValidationService.TagExistsAsync)
                .WithMessage("Tag with this id doesn't exists.")
                .When(dto => methodType == "PUT");

            RuleFor(r => r.Id)
                .MustAsync(tagValidationService.TagIdNotExistsAsync)
                .WithMessage("Tag with this id already exists.")
                .When(dto => methodType == "POST");

            RuleFor(r => r.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync(tagValidationService.TagNameNotExistsAsync)
                .WithMessage("Tag with this name already exists.");

            RuleFor(r => r.CategoryId)
                .NotNull()
                .NotEmpty()
                .MustAsync(categoryValidationService.CategoryExistsAsync)
                .WithMessage("Category with this id doesn't exists.");
        }
    }
}