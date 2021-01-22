using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host
{
    public static class CrudServicesExtensions
    {
        public static IServiceCollection AddCrudServices(this IServiceCollection services)
        {
            services.AddTransient<IService<User>, UserService>();

            services.AddTransient<IService<History>, HistoryService>();

            services.AddTransient<IVendorService, VendorService>();

            //services.AddTransient<IService<PreOrder>, PreOrderService>();

            //services.AddTransient<IRepository<Vendor>, VendorRepository>();

            //services.AddTransient<ICategoryRepository, CategoryRepository>();

            //services.AddTransient<ITagRepository, TagRepository>();

            //services.AddTransient<IDiscountRepository, DiscountRepository>();
            return services;
        }
    }
}
