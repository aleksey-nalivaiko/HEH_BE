using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class CrudServicesExtensions
    {
        public static IServiceCollection AddCrudServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            //services.AddTransient<IService<PreOrder>, PreOrderService>();

            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped<IVendorService, VendorService>();

            services.AddScoped<IDiscountService, DiscountService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IFavoritesService, FavoritesService>();

            services.AddScoped<IHistoryService, HistoryService>();

            return services;
        }
    }
}
