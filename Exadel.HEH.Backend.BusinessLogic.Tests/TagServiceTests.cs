using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class TagServiceTests : BaseServiceTests<Tag>
    {
        private readonly TagService _service;
        private List<Category> _testCategories;
        private Tag _testTag;

        public TagServiceTests()
        {
            var discountRepository = new Mock<IDiscountRepository>();

            _service = new TagService(Repository.Object, discountRepository.Object, MapperExtensions.Mapper);

            InitTestData();
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            var newTag = new TagDto
            {
                Id = Guid.NewGuid(),
                Name = "TagName",
                CategoryId = Guid.NewGuid()
            };
            await _service.CreateAsync(newTag);
            var result = Data.Single(s => s.Id == newTag.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Data.Add(_testTag);
            var newTag = new TagDto
            {
                Id = _testTag.Id,
                CategoryId = Guid.NewGuid(),
                Name = "NewTagName"
            };

            await _service.UpdateAsync(newTag);
            Assert.Equal("NewTagName", Data.Single(x => x.Id == _testTag.Id).Name);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_testTag);
            await _service.RemoveAsync(_testTag.Id);
            Assert.Empty(Data);
        }

        private void InitTestData()
        {
            _testCategories = new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Category1"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Category2"
                }
            };
            _testTag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Tag1",
                CategoryId = _testCategories[0].Id
            };
        }
    }
}
