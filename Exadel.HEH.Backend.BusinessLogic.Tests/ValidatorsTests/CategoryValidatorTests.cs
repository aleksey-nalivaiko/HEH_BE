using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.BusinessLogic.Validators;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidatorsTests
{
    [ExcludeFromCodeCoverage]
    public class CategoryValidatorTests
    {
        private readonly Mock<IMethodProvider> _methodProvider;
        private readonly Mock<ICategoryValidationService> _validationService;

        private readonly List<CategoryDto> _categories;
        private CategoryValidator _categoryValidator;

        public CategoryValidatorTests()
        {
            _validationService = new Mock<ICategoryValidationService>();
            _methodProvider = new Mock<IMethodProvider>();

            _categoryValidator = new CategoryValidator(_validationService.Object, _methodProvider.Object);

            _categories = new List<CategoryDto>
            {
                new CategoryDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Food"
                },
                new CategoryDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Sport"
                }
            };

            _validationService.Setup(s => s.CategoryExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(_categories.Any(c => c.Id == id)));

            _validationService.Setup(s => s.CategoryIdNotExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(_categories.All(c => c.Id != id)));

            _validationService.Setup(s => s.CategoryNameNotExistsAsync(It.IsAny<string>(), CancellationToken.None))
                .Returns((string name, CancellationToken token) =>
                    Task.FromResult(_categories.All(c => c.Name != name)));

            _validationService.Setup(s => s.CategoryNameChangedAndNotExistsAsync(It.IsAny<Guid>(),
                    It.IsAny<string>(), CancellationToken.None))
                .Returns((Guid id, string name, CancellationToken token) =>
                {
                    var category = _categories.First(c => c.Id == id);
                    if (category.Name == name)
                    {
                        return Task.FromResult(true);
                    }

                    return Task.FromResult(_categories.All(c => c.Name != name));
                });
        }

        [Fact]
        public void CanValidateIdNotEmpty()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _categoryValidator = new CategoryValidator(_validationService.Object, _methodProvider.Object);

            var result = _categoryValidator.TestValidate(new CategoryDto
            {
                Id = Guid.Empty,
                Name = "Beauty"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateIdExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _categoryValidator = new CategoryValidator(_validationService.Object, _methodProvider.Object);

            var result = _categoryValidator.TestValidate(new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Beauty"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateIdNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("POST");
            _categoryValidator = new CategoryValidator(_validationService.Object, _methodProvider.Object);

            var result = _categoryValidator.TestValidate(new CategoryDto
            {
                Id = _categories[0].Id,
                Name = "Beauty"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateNameNotNull()
        {
            var result = _categoryValidator.TestValidate(new CategoryDto
            {
                Id = _categories[0].Id,
                Name = null
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameNotEmpty()
        {
            var result = _categoryValidator.TestValidate(new CategoryDto
            {
                Id = _categories[0].Id,
                Name = string.Empty
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameMaxLength()
        {
            var result = _categoryValidator.TestValidate(new CategoryDto
            {
                Id = _categories[0].Id,
                Name = new string('a', 51)
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("POST");
            _categoryValidator = new CategoryValidator(_validationService.Object, _methodProvider.Object);

            var result = _categoryValidator.TestValidate(new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Food"
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameChangedAndNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _categoryValidator = new CategoryValidator(_validationService.Object, _methodProvider.Object);

            var result = _categoryValidator.TestValidate(new CategoryDto
            {
                Id = _categories[1].Id,
                Name = "Food"
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
    }
}