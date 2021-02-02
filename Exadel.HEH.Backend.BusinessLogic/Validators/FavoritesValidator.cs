using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class FavoritesValidator : AbstractValidator<FavoritesDto>
    {
        public FavoritesValidator(IFavoritesValidationService favoritesValidationService)
        {
            // TODO: remove constant string to enother place
            RuleFor(f => f.Id).NotEmpty().NotNull()
                .MustAsync(favoritesValidationService.ValidateDiscountId).WithMessage("This discount id doesn't exists.")
                .MustAsync(favoritesValidationService.ValidateUserFavorites).WithMessage("Such favorites already exists.");
            RuleFor(f => f.Note).MaximumLength(255);
        }
    }
}
