using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class MethodProviderExtension
    {
        public static IServiceCollection AddMethodProvider(this IServiceCollection services)
        {
            services.AddScoped<IMethodProvider, MethodProvider>();
            return services;
        }
    }
}
