using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class FavoritesValidator : AbstractValidator<FavoritesShortDto>
    {
        public FavoritesValidator(IFavoritesValidationService favoritesValidationService)
        {
            // TODO: remove constant string to enother place
            RuleFor(f => f.DiscountId).Cascade(CascadeMode.Stop).NotEmpty().NotNull()
                .MustAsync(favoritesValidationService.ValidateDiscountIdIsExist).WithMessage("This discount id doesn't exists.")
                .MustAsync(favoritesValidationService.ValidateUserFavoritesIsExist).WithMessage("Such favorites already exists.");
            RuleFor(f => f.Note).MaximumLength(255);
        }
    }
}
