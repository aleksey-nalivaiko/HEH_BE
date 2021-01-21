using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public void ServiseHaveLinkWithRepository()
        {
            // Arrange
            var mock = new Mock<ICategoryRepository>();
            mock.Setup(x => x.GetByTagAsync(Guid.NewGuid())).Returns(GetCategory);

            // Act
            var result = new CategoryService(mock.Object);

            // Assert
            Assert.NotNull(result.GetByTagAsync(Guid.NewGuid()));
        }

        private Task<Category> GetCategory()
        {
            Category category = new Category() { };
            return Task.FromResult(category);
        }
    }
}
