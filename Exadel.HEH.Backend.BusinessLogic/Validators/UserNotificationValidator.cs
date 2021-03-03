using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class UserNotificationValidator : AbstractValidator<UserNotificationDto>
    {
        public UserNotificationValidator(
            ICategoryValidationService categoryValidationService,
            ITagValidationService tagValidationService,
            IVendorValidationService vendorValidationService)
        {
            CascadeMode = CascadeMode.Stop;

            RuleForEach(n => n.TagNotifications)
                .MustAsync(tagValidationService.TagExistsAsync)
                .WithMessage("Some of tags don't exist")
                .When(n => n.TagNotifications != null);

            RuleForEach(n => n.CategoryNotifications)
                .MustAsync(categoryValidationService.CategoryExistsAsync)
                .WithMessage("Some of categories don't exist")
                .When(n => n.CategoryNotifications != null);

            RuleForEach(n => n.VendorNotifications)
                .MustAsync(vendorValidationService.VendorExistsAsync)
                .WithMessage("Some of vendors don't exist")
                .MustAsync(vendorValidationService.VendorFromLocationAsync)
                .WithMessage("Some of vendors are not from user location")
                .When(n => n.VendorNotifications != null);
        }
    }
}