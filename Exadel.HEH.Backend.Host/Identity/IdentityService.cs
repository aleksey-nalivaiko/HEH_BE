using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using IdentityServer4.Models;

namespace Exadel.HEH.Backend.Host.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task InitializeAsync()
        {
            await _identityRepository.RemoveAllAsync<Client>();
            foreach (var client in Clients.Get())
            {
                await _identityRepository.CreateAsync(client);
            }

            await _identityRepository.RemoveAllAsync<IdentityResource>();
            foreach (var item in Resources.GetIdentityResources())
            {
                await _identityRepository.CreateAsync(item);
            }

            await _identityRepository.RemoveAllAsync<ApiResource>();
            foreach (var item in Resources.GetApiResources())
            {
                await _identityRepository.CreateAsync(item);
            }

            await _identityRepository.RemoveAllAsync<ApiScope>();
            foreach (var item in Resources.GetApiScopes())
            {
                await _identityRepository.CreateAsync(item);
            }
        }
    }
}