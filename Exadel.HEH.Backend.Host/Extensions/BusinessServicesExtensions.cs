using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class BusinessServicesExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services,
            IWebHostEnvironment env)
        {
            services.AddScoped<IStatisticsService, StatisticsService>();

            services.AddSingleton<SchedulerService>();

            if (env.IsDevelopment())
            {
                services.AddScoped<ISearchService, LocalSearchService>();
            }
            else
            {
                services.AddScoped<ISearchService, LuceneSearchService>();
            }

            return services;
        }
    }
}