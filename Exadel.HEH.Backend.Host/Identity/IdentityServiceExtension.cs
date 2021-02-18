using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Identity
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddSingleton<IIdentityService, IdentityService>();

            return services;
        }
    }
}