using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class CategoryControllerTests
    {
        private readonly CategoryController _controller;
        private readonly List<CategoryDto> _categoryWithTagsData;
        private CategoryDto _testCategory;

        public CategoryControllerTests()
        {
            var service = new Mock<ICategoryService>();
            _controller = new CategoryController(service.Object);

            _categoryWithTagsData = new List<CategoryDto>();

            service.Setup(s => s.CreateAsync(It.IsAny<CategoryDto>()))
               .Callback((CategoryDto item) =>
               {
                   _categoryWithTagsData.Add(item);
               })
               .Returns(Task.CompletedTask);

            service.Setup(s => s.UpdateAsync(It.IsAny<CategoryDto>()))
                .Callback((CategoryDto item) =>
                {
                    var oldItem = _categoryWithTagsData.Single();
                    if (oldItem != null)
                    {
                        _categoryWithTagsData.Remove(oldItem);
                        _categoryWithTagsData.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            service.Setup(r => r.RemoveAsync(It.IsAny<Guid>()))
                .Callback((Guid id) => { _categoryWithTagsData.RemoveAt(0); })
                .Returns(Task.CompletedTask);

            service.Setup(s => s.GetCategoriesWithTagsAsync())
                .Returns(() => Task.FromResult((IEnumerable<CategoryDto>)_categoryWithTagsData));

            InitTestData();
        }

        [Fact]
        public async Task CanGetCategoriesWithTagsAsync()
        {
            _categoryWithTagsData.Add(_testCategory);
            var result = await _controller.GetCategoriesWithTagsAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _controller.CreateAsync(_testCategory);
            Assert.Single(_categoryWithTagsData);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            _categoryWithTagsData.Add(_testCategory);
            var newCategory = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "CategoryName",
                Tags = new List<TagDto>()
            };

            await _controller.UpdateAsync(newCategory);
            Assert.NotEqual(_categoryWithTagsData.Single().Name, _testCategory.Name);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            _categoryWithTagsData.Add(_testCategory);
            await _controller.RemoveAsync(_testCategory.Id);
            Assert.Empty(_categoryWithTagsData);
        }

        private void InitTestData()
        {
            _testCategory = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Category",
                Tags = new List<TagDto>()
            };
        }
    }
}