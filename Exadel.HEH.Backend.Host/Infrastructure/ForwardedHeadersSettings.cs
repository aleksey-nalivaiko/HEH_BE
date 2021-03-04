using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class ForwardedHeadersSettings
    {
        static ForwardedHeadersSettings()
        {
            //https://github.com/IdentityServer/IdentityServer4/issues/1331
            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();
            Options = forwardOptions;
        }

        public static ForwardedHeadersOptions Options { get; set; }
    }
}