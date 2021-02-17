using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class FavoritesValidator : AbstractValidator<FavoritesShortDto>
    {
        public FavoritesValidator(IFavoritesValidationService favoritesValidationService)
        {
            RuleFor(f => f.DiscountId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .MustAsync(favoritesValidationService.DiscountExists)
                .WithMessage("Discount with this id doesn't exists.")
                .MustAsync(favoritesValidationService.UserFavoritesNotExists)
                .WithMessage("Such favorites already exists.");

            RuleFor(f => f.Note).MaximumLength(255);
        }
    }
}
