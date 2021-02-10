using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class ValidationServicesExtensions
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IFavoritesValidationService, FavoritesValidationService>();

            services.AddScoped<ICategoryValidationService, CategoryValidationService>();
            services.AddScoped<ITagValidationService, TagValidationSevice>();
            services.AddScoped<IVendorValidationService, VendorValidationService>();

            services.AddScoped<IDiscountValidationService, DiscountValidationService>();

            return services;
        }
    }
}
