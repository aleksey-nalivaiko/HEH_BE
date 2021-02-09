using System;
using System.Collections.Generic;
using System.Text;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator(IUserValidationService userValidationService)
        {
            RuleFor(x => x.Id).NotNull().NotEmpty()
                .MustAsync(userValidationService.ValidateUserIdExists)
                .WithMessage("Such user id doesn't exist");
        }
    }
}
