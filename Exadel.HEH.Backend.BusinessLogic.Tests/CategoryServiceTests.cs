using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
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
        private Category _testCategory;

        public CategoryServiceTests()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            var tagRepository = new Mock<ITagRepository>();
            var historyService = new Mock<IHistoryService>();

            tagRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Tag>)_tagData));

            categoryRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Category>)Data));

            categoryRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));

            categoryRepository.Setup(r => r.CreateAsync(It.IsAny<Category>()))
                .Callback((Category item) => { Data.Add(item); })
                .Returns(Task.CompletedTask);

            categoryRepository.Setup(f => f.UpdateAsync(It.IsAny<Category>()))
                .Callback((Category item) =>
                {
                    var oldItem = Data.FirstOrDefault(x => x.Id == item.Id);
                    if (oldItem != null)
                    {
                        Data.Remove(oldItem);
                        Data.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            categoryRepository.Setup(r => r.RemoveAsync(It.IsAny<Guid>()))
                .Callback((Guid id) => { Data.RemoveAll(d => d.Id == id); })
                .Returns(Task.CompletedTask);

            _service = new CategoryService(categoryRepository.Object, historyService.Object, tagRepository.Object, MapperExtensions.Mapper);

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

        [Fact]
        public async Task CanCreateAsync()
        {
            var newCategory = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "CategoryName"
            };

            await _service.CreateAsync(newCategory);
            var result = Data.Single(u => u.Id == newCategory.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Data.Add(_testCategory);
            var newCategory = new CategoryDto
            {
                Id = _testCategory.Id,
                Name = "NewCategoryName"
            };

            await _service.UpdateAsync(newCategory);
            Assert.Equal("NewCategoryName", Data.Single(x => x.Id == _testCategory.Id).Name);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_testCategory);
            await _service.RemoveAsync(_testCategory.Id);
            Assert.Empty(Data);
        }

        private void InitTestData()
        {
            _testCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = "ACategoryName"
            };
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