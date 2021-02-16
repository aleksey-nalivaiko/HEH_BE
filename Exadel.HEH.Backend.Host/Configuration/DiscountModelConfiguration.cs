#nullable enable
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Configuration
{
    public class DiscountModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
        {
            builder.EntitySet<DiscountDto>("Discount");
        }
    }
}