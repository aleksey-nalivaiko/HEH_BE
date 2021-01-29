using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Exadel.HEH.Backend.Host.SwaggerFilters
{
    public class CountParameterOperationFilter : IOperationFilter
    {
        private const string ControllerName = "Discount";
        private const string CountParam = "$count";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor
                && descriptor.ControllerName.StartsWith(ControllerName))
            {
                var parameter = operation.Parameters.FirstOrDefault(p =>
                    string.Equals(p.Name, CountParam, StringComparison.OrdinalIgnoreCase));
                if (parameter != null)
                {
                    parameter.Schema.Default = null;
                }
            }
        }
    }
}