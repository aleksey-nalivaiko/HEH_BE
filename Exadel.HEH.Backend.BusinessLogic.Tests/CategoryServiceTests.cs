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
        private readonly List<TagDto> _tagData;
        private List<Category> _categories;
        private List<TagDto> _tags;
        private Category _category;

        public CategoryServiceTests()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            var tagService = new Mock<ITagService>();
            var historyService = new Mock<IHistoryService>();
            var userService = new Mock<IUserService>();

            tagService.Setup(s => s.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<TagDto>)_tagData));

            categoryRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Category>)Data));

            categoryRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));

            categoryRepository.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) => Task.FromResult(Data.Where(x => ids.Contains(x.Id))));

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

            _service = new CategoryService(categoryRepository.Object, historyService.Object,
                tagService.Object, MapperExtensions.Mapper,
                userService.Object);

            _tagData = new List<TagDto>();

            InitTestData();
        }

        [Fact]
        public async Task CanGetCategoriesWithTagsAsync()
        {
            Data.AddRange(_categories);
            _tagData.AddRange(_tags);

            var result = await _service.GetCategoriesWithTagsAsync();

            Assert.Collection(result, category => Assert.Collection(category.Tags, Assert.NotNull, Assert.NotNull),
                category => Assert.Single(category.Tags));
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Data.Add(_category);

            var result = await _service.GetByIdAsync(_category.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanGetByIdsAsync()
        {
            Data.Add(_category);

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Food"
            };
            Data.Add(category);

            var result = await _service.GetByIdsAsync(new List<Guid> { _category.Id, category.Id });

            Assert.NotNull(result);
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
            Data.Add(_category);
            var newCategory = new CategoryDto
            {
                Id = _category.Id,
                Name = "NewCategoryName"
            };

            await _service.UpdateAsync(newCategory);
            Assert.Equal("NewCategoryName", Data.Single(x => x.Id == _category.Id).Name);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_category);
            await _service.RemoveAsync(_category.Id);
            Assert.Empty(Data);
        }

        private void InitTestData()
        {
            _category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "ACategoryName"
            };
            _categories = new List<Category>
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
            _tags = new List<TagDto>
            {
                new TagDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Tag1",
                    CategoryId = _categories[0].Id
                },
                new TagDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Tag2",
                    CategoryId = _categories[0].Id
                },
                new TagDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Tag3",
                    CategoryId = _categories[1].Id
                }
            };
        }
    }
}