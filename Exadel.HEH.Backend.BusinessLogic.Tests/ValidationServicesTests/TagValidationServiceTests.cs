using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidationServicesTests
{
    public class TagValidationServiceTests
    {
        private readonly TagValidationService _validationService;
        private readonly List<Guid> _tagToValidate;
        private readonly Tag _tag1;
        private readonly Tag _tag2;

        public TagValidationServiceTests()
        {
            var tagRepository = new Mock<ITagRepository>();

            _tag1 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Tag 1"
            };

            _tag2 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Tag 2"
            };

            var tag3 = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Tag 3"
            };

            var tagData = new List<Tag> { _tag1, _tag2, tag3 };
            _tagToValidate = new List<Guid> { _tag1.Id, _tag2.Id };

            tagRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(tagData.FirstOrDefault(t => t.Id == id)));
            tagRepository.Setup(r => r.GetAsync(t => true))
                .Returns(() => Task.FromResult((IEnumerable<Tag>)tagData));
            tagRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Tag>)tagData));

            _validationService = new TagValidationService(tagRepository.Object);
        }

        [Fact]
        public async Task CanValidateTagExists()
        {
            Assert.True(await _validationService.TagExistsAsync(_tag1.Id, CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateTagNotExists()
        {
            Assert.True(await _validationService.TagIdNotExistsAsync(Guid.NewGuid(), CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateTagsExist()
        {
            Assert.True(await _validationService.TagsExistsAsync(_tagToValidate, CancellationToken.None));
            Assert.True(await _validationService.TagsExistsAsync(new List<Guid>(), CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateNameNotExists()
        {
            Assert.True(await _validationService.TagNameNotExistsAsync("Test name", CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateTagNameChangedAndNotExists()
        {
            Assert.True(await _validationService.TagNameChangedAndNotExistsAsync(_tag1.Id, "Tag 4", CancellationToken.None));
            Assert.False(!await _validationService.TagNameChangedAndNotExistsAsync(_tag2.Id, "Tag 2", CancellationToken.None));
        }
    }
}