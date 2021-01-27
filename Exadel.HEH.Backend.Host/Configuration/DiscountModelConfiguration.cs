#nullable enable
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Configuration
{
    public class DiscountModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
        {
            builder.EntitySet<DiscountDto>("Discount").EntityType.HasKey(d => d.Id);
            builder.ComplexType<AddressDto>();
            builder.ComplexType<PhoneDto>();
        }
    }
}