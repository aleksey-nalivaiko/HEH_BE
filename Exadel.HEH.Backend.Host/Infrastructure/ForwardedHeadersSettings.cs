using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    public static class ForwardedHeadersSettings
    {
        static ForwardedHeadersSettings()
        {
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