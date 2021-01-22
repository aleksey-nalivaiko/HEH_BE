﻿using Exadel.HEH.Backend.DataAccess;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbContext>(provider =>
                new MongoDbContext(configuration.GetConnectionString("MongoConnection")));

            services.AddTransient<IRepository<User>, UserRepository>();

            services.AddTransient<IRepository<History>, HistoryRepository>();

            services.AddTransient<IRepository<PreOrder>, PreOrderRepository>();

            services.AddTransient<IRepository<Vendor>, VendorRepository>();

            services.AddTransient<ITagRepository, TagRepository>();

            services.AddTransient<IDiscountRepository, DiscountRepository>();

            return services;
        }
    }
}
