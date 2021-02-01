using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Exadel.HEH.Backend.Host.Identity.Store
{
    public class CustomClientStore : IClientStore
    {
        private readonly IIdentityRepository _repository;

        public CustomClientStore(IIdentityRepository repository)
        {
            _repository = repository;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return _repository.GetOneAsync<Client>(x => x.ClientId == clientId);
        }
    }
}