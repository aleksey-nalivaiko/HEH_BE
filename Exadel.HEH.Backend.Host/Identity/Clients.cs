using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using IdentityServer4.Models;

namespace Exadel.HEH.Backend.Host.Identity
{
    [ExcludeFromCodeCoverage]
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
                    }
                }
            };
        }
    }
}