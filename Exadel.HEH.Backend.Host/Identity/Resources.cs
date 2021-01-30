using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace Exadel.HEH.Backend.Host.Identity
{
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
                new ApiResource("exadel_heh_api", "HEH Api")
                {
                    Scopes = { "exadel_heh_api" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("exadel_heh_api", "Full access to HEH Api")
            };
        }
    }
}