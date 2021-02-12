using Exadel.HEH.Backend.BusinessLogic;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
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

            services.AddSingleton<IEmailService, EmailService>();

            if (env.IsDevelopment())
            {
                services.AddScoped<ISearchService, LocalSearchService>();
            }
            else
            {
                services.AddScoped<ISearchService, LuceneSearchService>();
            }

            services.AddScoped<ISearchEventHub, SearchEventHub>();

            services.AddScoped<ISearchEventSubscriber, SearchEventSubscriber>();

            return services;
        }
    }
}