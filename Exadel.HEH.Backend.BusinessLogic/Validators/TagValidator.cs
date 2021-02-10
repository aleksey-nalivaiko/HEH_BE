using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
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
            var methodType = methodProvider.GetMethodUpperName();

            RuleFor(r => r.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MustAsync(tagValidationService.TagExistsAsync)
                .WithMessage("Tag with this id doesn't exists.")
                .When(dto => methodType == "PUT");

            RuleFor(r => r.Id)
                .MustAsync(tagValidationService.TagNotExistsAsync)
                .WithMessage("Tag with this id already exists.")
                .When(dto => methodType == "POST");

            RuleFor(r => r.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(r => r.CategoryId)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty()
                .MustAsync(categoryValidationService.CategoryExistsAsync)
                .WithMessage("Category with this id doesn't exists.");
        }
    }
}