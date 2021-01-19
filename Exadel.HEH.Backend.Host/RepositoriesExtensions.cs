using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            services.AddTransient<IUserRepository>(provider =>
                new UserRepository(connectionString));

            services.AddTransient<IHistoryRepository>(provider =>
                new HistoryRepository(connectionString));

            return services;
        }
    }
}
