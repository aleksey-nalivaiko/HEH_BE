using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class CrudServicesExtensions
    {
        public static IServiceCollection AddCrudServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IService<HistoryDto>, HistoryService>();

            //services.AddTransient<IService<PreOrder>, PreOrderService>();

            services.AddScoped<IService<LocationDto>, LocationService>();

            services.AddScoped<IVendorService, VendorService>();

            services.AddScoped<IDiscountService, DiscountService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IFavoritesService, FavoritesService>();

            services.AddScoped<ICategoryValidationService, CategoryValidationService>();

            return services;
        }
    }
}
