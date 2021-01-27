using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Exadel.HEH.Backend.Host
{
    public class CustomDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            Assembly assembly = typeof(ODataController).Assembly;
            var thisAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes().ToList();
            var odatacontrollers = thisAssemblyTypes
                .Where(t => t.BaseType == typeof(Microsoft.AspNet.OData.ODataController)).ToList();
            var odatamethods = new[] { "Get", "Put", "Post", "Delete" };

            foreach (var odataContoller in odatacontrollers)
            {
                var methods = odataContoller.GetMethods().Where(a => odatamethods.Contains(a.Name)).ToList();
                if (!methods.Any())
                {
                    continue;
                }
            }
        }

        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<DiscountDto>("Discounts");
            odataBuilder.ComplexType<AddressDto>();
            odataBuilder.ComplexType<PhoneDto>();

            return odataBuilder.GetEdmModel();
        }
    }
}