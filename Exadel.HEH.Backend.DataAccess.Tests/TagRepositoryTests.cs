﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class TagRepositoryTests : BaseRepositoryTests<Tag>
    {
        private readonly TagRepository _repository;
        private readonly Tag _tag;

        public TagRepositoryTests()
        {
            _repository = new TagRepository(Context.Object);
            _tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "TagName",
                CategoryId = Guid.NewGuid()
            };
        }

        [Fact]
        public async Task CanGetAll()
        {
            Collection.Add(_tag);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Collection.Add(_tag);

            var result = await _repository.GetByIdAsync(_tag.Id);
            Assert.Equal(_tag, result);
        }

        [Fact]
        public async Task CanUpdate()
        {
            Collection.Add(_tag.DeepClone());
            _tag.Name = "NewCategoryName";

            await _repository.UpdateAsync(_tag);
            Assert.Equal("NewCategoryName", Collection.Single(x => x.Id == _tag.Id).Name);
        }

        [Fact]
        public async Task CanRemoveById()
        {
            Collection.Add(_tag);

            await _repository.RemoveAsync(_tag.Id);
            Assert.Empty(Collection);
        }

        [Fact]
        public async Task CanCreate()
        {
            await _repository.CreateAsync(_tag);
            Assert.Single(Collection);
        }
    }
}
