#nullable enable
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Configuration
{
    public class UserModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
        {
            builder.EntitySet<UserShortDto>("User");
            builder.ComplexType<AddressDto>();
        }
    }
}