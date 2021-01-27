using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Exadel.HEH.Backend.Host
{
    public class CountParameterOperationFilter : IOperationFilter
    {
        private const string ControllerName = "Discount";
        private const string ParameterName = "$count";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor
                && descriptor.ControllerName.StartsWith(ControllerName))
            {
                var parameter = operation.Parameters.FirstOrDefault(p => p.Name == ParameterName);
                if (parameter != null)
                {
                    parameter.Schema.Default = null;
                }
            }
        }
    }
}