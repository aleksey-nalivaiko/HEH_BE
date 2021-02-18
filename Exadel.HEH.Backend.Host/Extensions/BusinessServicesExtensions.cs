using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
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

            //if (env.IsDevelopment())
            //{
            //    services.AddScoped<ISearchService<Discount, Discount>, LocalDiscountSearchService>();
            //    services.AddScoped<ISearchService<VendorSearch, VendorDto>, LocalVendorSearchService>();
            //}
            //else
            //{
            services.AddScoped<ISearchService<Discount, Discount>, LuceneDiscountSearchService>();
            services.AddScoped<ISearchService<VendorSearch, VendorDto>, LuceneVendorSearchService>();
            //}

            return services;
        }
    }
}