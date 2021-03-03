using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.BusinessLogic.Validators;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidatorsTests
{
    public class FavoritesValidatorTests
    {
        private readonly FavoritesShortDto _favorites;
        private readonly FavoritesValidator _favoritesValidator;

        public FavoritesValidatorTests()
        {
            var discountValidationService = new Mock<IDiscountValidationService>();
            var favoritesValidationService = new Mock<IFavoritesValidationService>();

            _favoritesValidator = new FavoritesValidator(favoritesValidationService.Object,
                discountValidationService.Object);

            var discounts = new List<DiscountDto>
            {
                new DiscountDto
                {
                    Id = Guid.NewGuid()
                }
            };

            _favorites = new FavoritesShortDto
            {
                DiscountId = discounts[0].Id
            };

            var user = new UserDto
            {
                Favorites = new List<FavoritesShortDto>
                {
                    _favorites
                }
            };

            discountValidationService.Setup(s => s.DiscountExists(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(discounts.Any(c => c.Id == id)));

            favoritesValidationService.Setup(s => s.UserFavoritesNotExists(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(user.Favorites.All(f => f.DiscountId != id)));
        }

        [Fact]
        public void CanValidateIdExists()
        {
            var result = _favoritesValidator.TestValidate(new FavoritesShortDto
            {
                DiscountId = Guid.NewGuid()
            });
            result.ShouldHaveValidationErrorFor(f => f.DiscountId);
        }

        [Fact]
        public void CanValidateUserFavoritesNotExists()
        {
            var result = _favoritesValidator.TestValidate(_favorites);
            result.ShouldHaveValidationErrorFor(f => f.DiscountId);
        }

        [Fact]
        public void CanValidateNoteMaxLength()
        {
            var result = _favoritesValidator.TestValidate(new FavoritesShortDto
            {
                DiscountId = Guid.NewGuid(),
                Note = new string('a', 256)
            });
            result.ShouldHaveValidationErrorFor(f => f.Note);
        }
    }
}