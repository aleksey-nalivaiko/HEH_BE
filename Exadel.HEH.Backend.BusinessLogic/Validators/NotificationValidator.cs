using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using FluentValidation;

namespace Exadel.HEH.Backend.BusinessLogic.Validators
{
    public class NotificationValidator : AbstractValidator<UserNotificationDto>
    {
        public NotificationValidator(ICategoryValidationService categoryValidationService,
            ITagValidationService tagValidationService, IVendorValidationService vendorValidationService,
            IMethodProvider methodProvider)
        {
            var methodType = methodProvider.GetMethodUpperName();

            When(dto => methodType == "PUT", () =>
            {
                RuleForEach(n => n.TagNotifications)
                    .NotNull()
                    .NotEmpty()
                    .MustAsync(tagValidationService.TagExistsAsync)
                    .WithName("TagId")
                    .WithMessage("Tag id doesn't exist");

                RuleForEach(n => n.CategoryNotifications)
                    .NotNull()
                    .NotEmpty()
                    .MustAsync(categoryValidationService.CategoryExistsAsync)
                    .WithName("CategoryId")
                    .WithMessage("Category id doesn't exist");

                RuleForEach(n => n.VendorNotifications)
                    .NotNull()
                    .NotEmpty()
                    .MustAsync(vendorValidationService.VendorExistsAsync)
                    .WithName("VendorId")
                    .WithMessage("Vendor id doesn't exist");
            });
        }
    }
}