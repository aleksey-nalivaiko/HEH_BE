using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories;
using IdentityServer4.Models;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class IdentityRepositoryTests
    {
        private readonly IdentityRepository _repository;
        private readonly List<IdentityResource> _collection;

        public IdentityRepositoryTests()
        {
            _collection = new List<IdentityResource>();
            var context = new Mock<IDbContext>();

            _repository = new IdentityRepository(context.Object);

            context.Setup(c => c.GetAll<IdentityResource>())
                .Returns(_collection.AsQueryable());

            context.Setup(c => c.GetAsync(It.IsAny<Expression<Func<IdentityResource, bool>>>()))
                .Returns((Expression<Func<IdentityResource, bool>> expression) =>
                    Task.FromResult(_collection.Where(expression.Compile())));

            context.Setup(c => c.GetOneAsync(It.IsAny<Expression<Func<IdentityResource, bool>>>()))
                .Returns((Expression<Func<IdentityResource, bool>> expression) =>
                    Task.FromResult(_collection.First(expression.Compile())));

            context.Setup(c => c.RemoveAsync(It.IsAny<Expression<Func<IdentityResource, bool>>>()))
                .Callback((Expression<Func<IdentityResource, bool>> expression) =>
                {
                    _collection.RemoveAll(x => expression.Compile()(x));
                })
                .Returns(Task.CompletedTask);

            context.Setup(c => c.RemoveAllAsync<IdentityResource>())
                .Callback(_collection.Clear)
                .Returns(Task.CompletedTask);

            context.Setup(c => c.CreateAsync(It.IsAny<IdentityResource>()))
                .Callback((IdentityResource doc) =>
                {
                    _collection.Add(doc);
                })
                .Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            _collection.Add(new IdentityResource());

            var result = await _repository.GetAllAsync<IdentityResource>();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetAsync()
        {
            _collection.Add(new IdentityResource("api", new List<string>
            {
                "role"
            }));

            var result = await _repository.GetAsync<IdentityResource>(c => c.Name == "api");
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetOneAsync()
        {
            _collection.Add(new IdentityResource("api", new List<string>
            {
                "role"
            }));

            var result = await _repository.GetOneAsync<IdentityResource>(c => c.Name == "api");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _repository.CreateAsync(new IdentityResource());
            Assert.Single(_collection);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            _collection.Add(new IdentityResource("api", new List<string>
            {
                "role"
            }));

            await _repository.RemoveAsync<IdentityResource>(i => i.Name == "api");
            Assert.Empty(_collection);
        }

        [Fact]
        public async Task CanRemoveAllAsync()
        {
            _collection.Add(new IdentityResource());

            await _repository.RemoveAllAsync<IdentityResource>();
            Assert.Empty(_collection);
        }
    }
}