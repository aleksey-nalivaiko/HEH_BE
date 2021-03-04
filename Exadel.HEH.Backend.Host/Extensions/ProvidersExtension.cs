using System.Diagnostics.CodeAnalysis;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ProvidersExtension
    {
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<IUserProvider, UserProvider>();

            services.AddScoped<IMethodProvider, MethodProvider>();

            return services;
        }
    }
}