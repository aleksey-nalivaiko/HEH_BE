using Exadel.HEH.Backend.BusinessLogic;
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