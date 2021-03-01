#nullable enable
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Configuration
{
    public class StatisticsModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
        {
            builder.EntitySet<DiscountStatisticsDto>("Statistics");

            builder.EntityType<DiscountStatisticsDto>().Collection
                .Function("Excel")
                .Returns<Task<FileResult>>();
        }
    }
}