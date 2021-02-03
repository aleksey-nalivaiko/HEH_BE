using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.BusinessLogic.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<FavoritesCreateUpdateDto>, FavoritesValidator>();

            return services;
        }
    }
}
