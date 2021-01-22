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
        private readonly List<CategoryWithTagsDto> _categoryWithTagsData;
        private CategoryWithTagsDto _testCategoryWithTags;

        public CategoryControllerTests()
        {
            var service = new Mock<ICategoryService>();
            _controller = new CategoryController(service.Object);

            _categoryWithTagsData = new List<CategoryWithTagsDto>();

            service.Setup(s => s.GetCategoriesWithTagsAsync())
                .Returns(() => Task.FromResult((IEnumerable<CategoryWithTagsDto>)_categoryWithTagsData));

            InitTestData();
        }

        [Fact]
        public async Task CanGetCategoriesWithTagsAsync()
        {
            _categoryWithTagsData.Add(_testCategoryWithTags);
            var result = await _controller.GetCategoriesWithTagsAsync();
            Assert.Single(result);
        }

        private void InitTestData()
        {
            _testCategoryWithTags = new CategoryWithTagsDto
            {
                Id = Guid.NewGuid(),
                Name = "Category",
                Tags = new List<TagDto>()
            };
        }
    }
}