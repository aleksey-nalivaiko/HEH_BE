using Exadel.HEH.Backend.BusinessLogic;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class UserProviderExtension
    {
        public static IServiceCollection AddUserProvider(this IServiceCollection services)
        {
            services.AddScoped<IUserProvider, UserProvider>();
            return services;
        }
    }
}