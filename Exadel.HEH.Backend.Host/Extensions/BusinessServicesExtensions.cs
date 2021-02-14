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

            services.AddSingleton<IEmailService, EmailService>();

            if (env.IsDevelopment())
            {
                services.AddScoped<IDiscountSearchService, LocalDiscountSearchService>();
            }
            else
            {
                services.AddScoped<IDiscountSearchService, LuceneDiscountSearchService>();
            }

            return services;
        }
    }
}