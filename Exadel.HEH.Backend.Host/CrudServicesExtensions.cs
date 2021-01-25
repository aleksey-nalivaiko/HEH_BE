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

            services.AddTransient<IDiscountService, DiscountService>();

            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<IService<PreOrder>, PreOrderService>();

            services.AddTransient<ITagService, TagService>();

            return services;
        }
    }
}
