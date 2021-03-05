using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Exadel.HEH.Backend.Host.Identity.Store
{
    public class CustomPersistedGrantStore : IPersistedGrantStore
    {
        private readonly IIdentityRepository _repository;

        public CustomPersistedGrantStore(IIdentityRepository repository)
        {
            _repository = repository;
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            return _repository.GetOneAsync<PersistedGrant>(p => p.Key == key);
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            return _repository.GetAsync<PersistedGrant>(p => p.SubjectId == filter.SubjectId);
        }

        public Task RemoveAsync(string key)
        {
            return _repository.RemoveAsync<PersistedGrant>(p => p.Key == key);
        }

        public Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            return _repository.RemoveAsync<PersistedGrant>(p => p.ClientId == filter.ClientId
                                                                && p.SubjectId == filter.SubjectId
                                                                && p.Type == filter.Type);
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            return _repository.CreateAsync(grant);
        }
    }
}