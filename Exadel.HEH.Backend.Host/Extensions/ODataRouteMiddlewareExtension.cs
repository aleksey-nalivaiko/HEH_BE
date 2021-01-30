using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class ODataRouteMiddlewareExtension
    {
        public static IApplicationBuilder UseODataRouting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ODataRouteMiddleware>();
        }
    }
}