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
        public async Task CanGetAllAsync()
        {
            Collection.Add(_tag);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetAsync()
        {
            Collection.Add(_tag);

            var result = await _repository.GetAsync(tag => tag.Name == "TagName");
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Collection.Add(_tag);

            var result = await _repository.GetByIdAsync(_tag.Id);
            Assert.Equal(_tag, result);
        }

        [Fact]
        public async Task CanGetByIdsAsync()
        {
            Collection.Add(_tag);
            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Pizza"
            };
            Collection.Add(tag);

            var result = await _repository.GetByIdsAsync(new List<Guid> { _tag.Id, tag.Id });

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Collection.Add(_tag.DeepClone());
            _tag.Name = "NewCategoryName";

            await _repository.UpdateAsync(_tag);
            Assert.Equal("NewCategoryName", Collection.Single(x => x.Id == _tag.Id).Name);
        }

        [Fact]
        public async Task CanRemoveByIdAsync()
        {
            Collection.Add(_tag);

            await _repository.RemoveAsync(_tag.Id);
            Assert.Empty(Collection);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Collection.Add(_tag);

            await _repository.RemoveAsync(t => t.Id == _tag.Id);
            Assert.Empty(Collection);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _repository.CreateAsync(_tag);
            Assert.Single(Collection);
        }
    }
}
