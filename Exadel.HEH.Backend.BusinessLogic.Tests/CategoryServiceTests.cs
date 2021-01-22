using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class CategoryServiceTests : BaseServiceTests<Category>
    {
        private readonly CategoryService _service;
        private readonly List<Tag> _tagData;
        private List<Category> _testCategories;
        private List<Tag> _testTags;

        public CategoryServiceTests()
        {
            var tagRepository = new Mock<IRepository<Tag>>();

            tagRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Tag>)_tagData));

            _service = new CategoryService(Repository.Object, tagRepository.Object, Mapper);

            _tagData = new List<Tag>();

            InitTestData();
        }

        [Fact]
        public async Task CanGetCategoriesWithTagsAsync()
        {
            Data.AddRange(_testCategories);
            _tagData.AddRange(_testTags);

            var result = await _service.GetCategoriesWithTagsAsync();

            Assert.Collection(result, category => Assert.Collection(category.Tags, Assert.NotNull, Assert.NotNull),
                category => Assert.Single(category.Tags));
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
            _testTags = new List<Tag>
            {
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Tag1",
                    CategoryId = _testCategories[0].Id
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Tag2",
                    CategoryId = _testCategories[0].Id
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "Tag3",
                    CategoryId = _testCategories[1].Id
                }
            };
        }
    }
}