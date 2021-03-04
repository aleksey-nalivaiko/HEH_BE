using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Exadel.HEH.Backend.Host.SwaggerFilters
{
    [ExcludeFromCodeCoverage]
    public class HideApiVersionOperationFilter : IOperationFilter
    {
        private const string ApiVersionParam = "api-version";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiParam = operation.Parameters.FirstOrDefault(p =>
                string.Equals(p.Name, ApiVersionParam, StringComparison.OrdinalIgnoreCase));
            if (apiParam != null)
            {
                operation.Parameters.Remove(apiParam);
            }
        }
    }
}