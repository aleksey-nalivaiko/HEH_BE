using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.Host
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton(provider =>
            {
                var client = new MongoClient(connectionString);
                return client.GetDatabase(new MongoUrlBuilder(connectionString).DatabaseName);
            });

            services.AddTransient<IRepository<User>, UserRepository>();

            services.AddTransient<IRepository<History>, HistoryRepository>();

            services.AddTransient<IRepository<PreOrder>, PreOrderRepository>();

            services.AddTransient<IRepository<Vendor>, VendorRepository>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<ITagRepository, TagRepository>();

            services.AddTransient<IDiscountRepository, DiscountRepository>();

            return services;
        }
    }
}
