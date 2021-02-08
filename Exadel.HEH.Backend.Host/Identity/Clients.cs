using System.Collections.Generic;
using IdentityServer4.Models;

namespace Exadel.HEH.Backend.Host.Identity
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "HEHApiClient",
                    ClientName = "Happy Exadel Hours Api client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedScopes = new[]
                    {
                        "heh_api"
                    },
                    AccessTokenLifetime = 3600 * 24
                }
            };
        }
    }
}