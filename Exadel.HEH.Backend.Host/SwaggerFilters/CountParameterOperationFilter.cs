using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Exadel.HEH.Backend.Host.SwaggerFilters
{
    public class CountParameterOperationFilter : IOperationFilter
    {
        private const string CountParam = "$count";
        private readonly List<string> _controllerNames = new List<string> { "Discount", "User", "Vendor", "Statistics" };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor
                && _controllerNames.Any(name => descriptor.ControllerName.StartsWith(name)))
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