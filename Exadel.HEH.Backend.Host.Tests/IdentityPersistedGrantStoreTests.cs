using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Exadel.HEH.Backend.Host.Identity.Store;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class IdentityPersistedGrantStoreTests
    {
        private readonly CustomPersistedGrantStore _store;
        private readonly IList<PersistedGrant> _grantData;
        private readonly PersistedGrant _grant;

        public IdentityPersistedGrantStoreTests()
        {
            var repository = new Mock<IIdentityRepository>();
            _grant = new PersistedGrant
            {
                Key = "Test key",
                SubjectId = Guid.NewGuid().ToString(),
                ClientId = Guid.NewGuid().ToString(),
                Type = "Test type"
            };
            _grantData = new List<PersistedGrant> { _grant };

            repository.Setup(r => r.GetOneAsync(It.IsAny<Expression<Func<PersistedGrant, bool>>>()))
                .Returns(() => Task.FromResult(_grantData.FirstOrDefault(x => x.Key == _grant.Key)));

            repository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<PersistedGrant, bool>>>()))
                .Returns(() => Task.FromResult(_grantData.Where(x => x.SubjectId == _grant.SubjectId)));

            repository.Setup(r => r.RemoveAsync(It.IsAny<Expression<Func<PersistedGrant, bool>>>()))
                .Callback(() =>
                {
                    _grantData.Remove(_grant);
                })
                .Returns(Task.CompletedTask);

            repository.Setup(r => r.CreateAsync(It.IsAny<PersistedGrant>()))
                .Callback(() =>
                {
                    _grantData.Add(_grant);
                })
                .Returns(Task.CompletedTask);

            _store = new CustomPersistedGrantStore(repository.Object);
        }

        [Fact]
        public async Task CanGetAsync()
        {
            Assert.NotNull(await _store.GetAsync(_grant.Key));
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Assert.NotEmpty(await _store.GetAllAsync(new PersistedGrantFilter { SubjectId = _grant.SubjectId }));
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            await _store.RemoveAsync(_grant.Key);
            Assert.Empty(_grantData);
        }

        [Fact]
        public async Task CanRemoveAllAsync()
        {
            var filter = new PersistedGrantFilter
            {
                ClientId = _grant.ClientId,
                SubjectId = _grant.SubjectId,
                Type = _grant.Type
            };

            await _store.RemoveAllAsync(filter);
        }

        [Fact]
        public async Task CanStoreAsync()
        {
            _grantData.Remove(_grant);
            await _store.StoreAsync(_grant);
            Assert.NotEmpty(_grantData);
        }
    }
}