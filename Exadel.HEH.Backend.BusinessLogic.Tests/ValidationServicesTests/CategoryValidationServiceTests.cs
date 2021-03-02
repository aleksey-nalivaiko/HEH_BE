using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Formatters;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidationServicesTests
{
    public class CategoryValidationServiceTests
    {
        private readonly CategoryValidationService _validationService;
        private readonly Category _category;

        public CategoryValidationServiceTests()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            var discountRepository = new Mock<IDiscountRepository>();

            var discountData = new List<Discount>();
            var categoryData = new List<Category>();
            _category = new Category { Id = Guid.NewGuid(), Name = "Test category" };
            categoryData.Add(_category);
            var discount = new Discount { CategoryId = _category.Id };
            discountData.Add(discount);

            categoryRepository.Setup(r =>
                    r.GetAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Category, bool>>>()))
                .Returns(() => Task.FromResult((IEnumerable<Category>)categoryData));

            categoryRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(categoryData.FirstOrDefault(x => x.Id == id)));

            discountRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Discount>)discountData));

            _validationService = new CategoryValidationService(categoryRepository.Object,
                discountRepository.Object);
        }

        [Fact]
        public async Task CanValidateCategoryExists()
        {
            Assert.True(await _validationService.CategoryExistsAsync(_category.Id, CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateCategoryIdNotExists()
        {
            Assert.True(!await _validationService.CategoryIdNotExistsAsync(_category.Id, CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateCategoryNameNotExists()
        {
            Assert.False(await _validationService.CategoryNameNotExistsAsync("Different name", CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateCategoryNameChangedAndNotExists()
        {
            Assert.True(await _validationService.CategoryNameChangedAndNotExistsAsync(_category.Id, _category.Name,
                    new CancellationToken(default)));
            Assert.False(await _validationService.CategoryNameChangedAndNotExistsAsync(_category.Id, "Different name",
                    new CancellationToken(default)));
        }

        [Fact]
        public async Task CanValidateCategoryNotInDiscount()
        {
            Assert.True(!await _validationService.CategoryNotInDiscountsAsync(_category.Id));
        }
    }
}