using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Exadel.HEH.Backend.Host.Identity.Store;
using IdentityServer4.Models;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class IdentityClientStoreTests
    {
        private readonly CustomClientStore _store;
        private readonly IList<Client> _clientData;
        private readonly Client _client;

        public IdentityClientStoreTests()
        {
            var repository = new Mock<IIdentityRepository>();
            _client = new Client
            {
                ClientId = Guid.NewGuid().ToString()
            };
            _clientData = new List<Client> { _client };

            repository.Setup(r => r.GetOneAsync(It.IsAny<Expression<Func<Client, bool>>>()))
                .Returns(() => Task.FromResult(_clientData.FirstOrDefault(x => x.ClientId == _client.ClientId)));

            _store = new CustomClientStore(repository.Object);
        }

        [Fact]
        public async Task CanFindClientByIdAsync()
        {
            var result = await _store.FindClientByIdAsync(_client.ClientId);
            Assert.Equal(_client.ClientId, result.ClientId);
        }
    }
}