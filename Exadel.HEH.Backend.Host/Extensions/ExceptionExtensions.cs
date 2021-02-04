using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace Exadel.HEH.Backend.Host.Extensions
{
    public static class ExceptionExtensions
    {
        public static IApplicationBuilder UseHttpStatusExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}