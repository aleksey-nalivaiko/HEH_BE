using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class CategoryRepositoryTests : BaseRepositoryTests<Category>
    {
        private readonly CategoryRepository _repository;

        private readonly Category _category;

        public CategoryRepositoryTests()
        {
            _repository = new CategoryRepository(Context.Object);
            _category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "CategoryName"
            };
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Collection.Add(_category);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetAsync()
        {
            Collection.Add(_category);

            var result = await _repository.GetAsync(c => c.Name == "CategoryName");
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Collection.Add(_category);

            var result = await _repository.GetByIdAsync(_category.Id);
            Assert.Equal(_category, result);
        }

        [Fact]
        public async Task CanGetByIdsAsync()
        {
            Collection.Add(_category);
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Food"
            };
            Collection.Add(category);

            var result = await _repository.GetByIdsAsync(new List<Guid> { _category.Id, category.Id });

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _repository.CreateAsync(_category);
            Assert.Single(Collection);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Collection.Add(_category.DeepClone());
            _category.Name = "NewCategoryName";

            await _repository.UpdateAsync(_category);
            Assert.Equal("NewCategoryName", Collection.Single(x => x.Id == _category.Id).Name);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Collection.Add(_category);

            await _repository.RemoveAsync(_category.Id);
            Assert.Empty(Collection);
        }
    }
}