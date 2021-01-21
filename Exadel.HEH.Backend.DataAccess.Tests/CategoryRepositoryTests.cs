﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class CategoryRepositoryTests : MongoRepositoryTests<Category>
    {
        private readonly CategoryRepository _repository;

        private readonly Category _category;
        private readonly Tag _tag;

        public CategoryRepositoryTests()
        {
            _repository = new CategoryRepository(Context.Object);
            _category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "CategoryName"
            };
            _tag = new Tag
            {
                Id = Guid.NewGuid(),
                CategoryId = _category.Id,
                Name = "TagName"
            };
        }

        [Fact]
        public async Task CanGetAll()
        {
            Collection.Add(_category);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Collection.Add(_category);

            var result = await _repository.GetByIdAsync(_category.Id);
            Assert.Equal(_category, result);
        }

        [Fact]
        public async Task CanUpdate()
        {
            Collection.Add(_category.DeepClone());
            _category.Name = "NewCategoryName";

            await _repository.UpdateAsync(_category.Id, _category);
            Assert.Equal("NewCategoryName", Collection.Single(x => x.Id == _category.Id).Name);
        }
    }
}
