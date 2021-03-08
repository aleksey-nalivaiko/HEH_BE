using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Exadel.HEH.Backend.Host.Identity;
using IdentityServer4.Models;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class IdentityServiceTests
    {
        private readonly IdentityService _service;
        private readonly IList<Client> _clientData;
        private readonly IList<IdentityResource> _identityData;
        private readonly IList<ApiResource> _resourceData;
        private readonly IList<ApiScope> _scopeData;

        public IdentityServiceTests()
        {
            var repository = new Mock<IIdentityRepository>();
            _clientData = new List<Client>();
            _identityData = new List<IdentityResource>();
            _resourceData = new List<ApiResource>();
            _scopeData = new List<ApiScope>();

            repository.Setup(r => r.GetAllAsync<Client>())
                .Returns(() => Task.FromResult(_clientData.AsEnumerable()));
            repository.Setup(r => r.RemoveAllAsync<Client>())
                .Callback(() => _clientData.ToList().RemoveAll(x => true))
                .Returns(Task.CompletedTask);
            repository.Setup(r => r.CreateAsync(It.IsAny<Client>()))
                .Callback((Client item) =>
                {
                    _clientData.Add(item);
                })
                .Returns(Task.CompletedTask);
            repository.Setup(r => r.CreateAsync(It.IsAny<IdentityResource>()))
                .Callback((IdentityResource item) =>
                {
                    _identityData.Add(item);
                })
                .Returns(Task.CompletedTask);
            repository.Setup(r => r.CreateAsync(It.IsAny<ApiResource>()))
                .Callback((ApiResource item) =>
                {
                    _resourceData.Add(item);
                })
                .Returns(Task.CompletedTask);
            repository.Setup(r => r.CreateAsync(It.IsAny<ApiScope>()))
                .Callback((ApiScope item) =>
                {
                    _scopeData.Add(item);
                })
                .Returns(Task.CompletedTask);

            _service = new IdentityService(repository.Object);
        }

        [Fact]
        public async Task CanInitialize()
        {
            await _service.InitializeAsync();
            Assert.NotEmpty(_clientData);
            Assert.NotEmpty(_identityData);
            Assert.NotEmpty(_resourceData);
            Assert.NotEmpty(_scopeData);
        }
    }
}