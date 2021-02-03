using System;
using System.Collections.Generic;
using System.Text;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty()
                .WithMessage("User id should not be null");
        }
    }
}
