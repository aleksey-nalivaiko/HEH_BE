using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Exadel.HEH.Backend.Host.Identity.Store
{
    public class CustomResourceStore : IResourceStore
    {
        private readonly IIdentityRepository _repository;

        public CustomResourceStore(IIdentityRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(
            IEnumerable<string> scopeNames)
        {
            return _repository.GetAsync<IdentityResource>(i => scopeNames.Contains(i.Name));
        }

        public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            return _repository.GetAsync<ApiScope>(a => scopeNames.Contains(a.Name));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            return _repository.GetAsync<ApiResource>(a => a.Scopes.Any(s => scopeNames.Contains(s)));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            return _repository.GetAsync<ApiResource>(a => apiResourceNames.Contains(a.Name));
        }

        public async Task<IdentityServer4.Models.Resources> GetAllResourcesAsync()
        {
            var identityResources = await _repository.GetAllAsync<IdentityResource>();
            var apiResources = await _repository.GetAllAsync<ApiResource>();
            var apiScopes = await _repository.GetAllAsync<ApiScope>();
            var result = new IdentityServer4.Models.Resources(identityResources, apiResources, apiScopes);
            return result;
        }
    }
}