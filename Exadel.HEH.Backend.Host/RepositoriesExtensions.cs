using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IMongoRepository<User>>(provider =>
                new UserRepository(connectionString));

            services.AddTransient<IMongoRepository<History>>(provider =>
                new HistoryRepository(connectionString));

            services.AddTransient<IMongoRepository<Tag>>(provider =>
                new TagRepository(connectionString));

            services.AddTransient<IMongoRepository<Category>>(provider =>
                new CategoryRepository(connectionString));

            return services;
        }
    }
}
