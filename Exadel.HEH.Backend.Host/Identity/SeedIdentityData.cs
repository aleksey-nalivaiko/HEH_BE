using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Identity
{
    public static class SeedIdentityData
    {
        public static async Task InitializeDatabaseAsync(IApplicationBuilder app)
        {
            var repository = app.ApplicationServices.GetService<IIdentityRepository>();

            if (repository != null)
            {
                if (!await repository.AnyAsync<Client>())
                {
                    foreach (var client in Clients.Get())
                    {
                        await repository.CreateAsync(client);
                    }
                }

                if (!await repository.AnyAsync<IdentityResource>())
                {
                    foreach (var item in Resources.GetIdentityResources())
                    {
                        await repository.CreateAsync(item);
                    }
                }

                if (!await repository.AnyAsync<ApiResource>())
                {
                    foreach (var item in Resources.GetApiResources())
                    {
                        await repository.CreateAsync(item);
                    }
                }

                if (!await repository.AnyAsync<ApiScope>())
                {
                    foreach (var item in Resources.GetApiScopes())
                    {
                        await repository.CreateAsync(item);
                    }
                }
            }
        }
    }
}