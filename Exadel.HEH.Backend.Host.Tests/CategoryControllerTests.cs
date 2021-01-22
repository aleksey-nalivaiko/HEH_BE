using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers;
using Exadel.HEH.Backend.Host.DTOs.Get;
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