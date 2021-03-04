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
    public class TagValidatorTests
    {
        private readonly Mock<IMethodProvider> _methodProvider;
        private readonly Mock<ITagValidationService> _tagValidationService;
        private readonly Mock<ICategoryValidationService> _categoryValidationService;

        private readonly List<TagDto> _tags;
        private TagValidator _tagValidator;

        public TagValidatorTests()
        {
            _tagValidationService = new Mock<ITagValidationService>();
            _categoryValidationService = new Mock<ICategoryValidationService>();
            _methodProvider = new Mock<IMethodProvider>();

            _tagValidator = new TagValidator(
                _tagValidationService.Object, _categoryValidationService.Object, _methodProvider.Object);

            _tags = new List<TagDto>
            {
                new TagDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Pizza"
                },
                new TagDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Sushi"
                }
            };

            _tagValidationService.Setup(s => s.TagExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(_tags.Any(c => c.Id == id)));

            _tagValidationService.Setup(s => s.TagIdNotExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(_tags.All(c => c.Id != id)));

            _tagValidationService.Setup(s => s.TagNameNotExistsAsync(It.IsAny<string>(), CancellationToken.None))
                .Returns((string name, CancellationToken token) =>
                    Task.FromResult(_tags.All(c => c.Name != name)));

            _tagValidationService.Setup(s => s.TagNameChangedAndNotExistsAsync(It.IsAny<Guid>(),
                    It.IsAny<string>(), CancellationToken.None))
                .Returns((Guid id, string name, CancellationToken token) =>
                {
                    var category = _tags.First(c => c.Id == id);
                    if (category.Name == name)
                    {
                        return Task.FromResult(true);
                    }

                    return Task.FromResult(_tags.All(c => c.Name != name));
                });

            var categories = new List<CategoryDto>
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

            _categoryValidationService.Setup(s => s.CategoryExistsAsync(It.IsAny<Guid>(), CancellationToken.None))
                .Returns((Guid id, CancellationToken token) =>
                    Task.FromResult(categories.Any(c => c.Id == id)));
        }

        [Fact]
        public void CanValidateIdNotEmpty()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _tagValidator = new TagValidator(_tagValidationService.Object, _categoryValidationService.Object,
                _methodProvider.Object);

            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = Guid.Empty,
                Name = "Pizza"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateIdExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _tagValidator = new TagValidator(_tagValidationService.Object, _categoryValidationService.Object,
                _methodProvider.Object);

            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = Guid.NewGuid(),
                Name = "Pizza"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateIdNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("POST");
            _tagValidator = new TagValidator(_tagValidationService.Object, _categoryValidationService.Object,
                _methodProvider.Object);

            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = _tags[0].Id,
                Name = "Pizza"
            });
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CanValidateNameNotNull()
        {
            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = _tags[0].Id,
                Name = null
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameNotEmpty()
        {
            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = _tags[0].Id,
                Name = string.Empty
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameMaxLength()
        {
            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = _tags[0].Id,
                Name = new string('a', 51)
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("POST");
            _tagValidator = new TagValidator(_tagValidationService.Object, _categoryValidationService.Object,
                _methodProvider.Object);

            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = Guid.NewGuid(),
                Name = "Pizza"
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateNameChangedAndNotExists()
        {
            _methodProvider.Setup(p => p.GetMethodUpperName()).Returns("PUT");
            _tagValidator = _tagValidator = new TagValidator(_tagValidationService.Object, _categoryValidationService.Object,
                _methodProvider.Object);

            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = _tags[1].Id,
                Name = "Pizza"
            });
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void CanValidateCategoryNotExists()
        {
            var result = _tagValidator.TestValidate(new TagDto
            {
                Id = Guid.NewGuid(),
                Name = "Hamburger",
                CategoryId = Guid.NewGuid()
            });
            result.ShouldHaveValidationErrorFor(c => c.CategoryId);
        }
    }
}