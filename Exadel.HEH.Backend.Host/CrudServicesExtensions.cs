using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
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
            services.AddTransient<IService<UserDto>, UserService>();

            services.AddTransient<IService<HistoryDto>, HistoryService>();

            //services.AddTransient<IService<PreOrder>, PreOrderService>();

            services.AddTransient<IService<LocationDto>, LocationService>();

            services.AddTransient<IService<VendorDto>, VendorService>();

            services.AddTransient<IDiscountService, DiscountService>();

            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<ITagService, TagService>();

            return services;
        }
    }
}
