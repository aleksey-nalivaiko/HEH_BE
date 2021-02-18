﻿using Exadel.HEH.Backend.DataAccess;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbContext>(provider =>
                new MongoDbContext(configuration["MongoConnection"]));

            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton<IRepository<History>, HistoryRepository>();

            services.AddSingleton<IRepository<PreOrder>, PreOrderRepository>();

            services.AddSingleton<IVendorRepository, VendorRepository>();

            services.AddSingleton<ILocationRepository, LocationRepository>();

            services.AddSingleton<ITagRepository, TagRepository>();

            services.AddSingleton<IDiscountRepository, DiscountRepository>();

            services.AddSingleton<ICategoryRepository, CategoryRepository>();

            services.AddSingleton<IIdentityRepository, IdentityRepository>();

            services.AddSingleton<IHistoryRepository, HistoryRepository>();

            services.AddSingleton<ISearchRepository<DiscountSearch>,
                SearchRepository<DiscountSearch>>();

            services.AddSingleton<ISearchRepository<VendorSearch>,
                SearchRepository<VendorSearch>>();

            return services;
        }
    }
}
