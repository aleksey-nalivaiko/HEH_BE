using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using IdentityModel;
using IdentityServer4.Models;

namespace Exadel.HEH.Backend.Host.Identity
{
    [ExcludeFromCodeCoverage]
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = JwtClaimTypes.Role,
                    UserClaims = new List<string> { JwtClaimTypes.Role }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("heh_api", "HEH Api")
                {
                    Scopes = { "heh_api" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("heh_api", "Full access to HEH Api")
            };
        }
    }
}