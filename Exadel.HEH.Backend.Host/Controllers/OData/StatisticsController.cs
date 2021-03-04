using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers.OData
{
    [ExcludeFromCodeCoverage]
    [ODataRoutePrefix("Statistics")]
    [ODataAuthorize(Roles = nameof(UserRole.Administrator))]
    public class StatisticsController : ODataController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [EnableQuery(
            HandleNullPropagation = HandleNullPropagationOption.False,
            EnsureStableOrdering = false)]
        [ODataRoute]
        public Task<IQueryable<DiscountStatisticsDto>> GetAsync([FromQuery] string searchText,
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
            ODataQueryOptions<DiscountStatisticsDto> options)
        {
            return _statisticsService.GetStatisticsAsync(options, searchText, startDate, endDate);
        }

        [EnableQuery(
            HandleNullPropagation = HandleNullPropagationOption.False,
            EnsureStableOrdering = false)]
        [HttpGet]
        [ODataActionFilter]
        public Task<IQueryable<DiscountStatisticsDto>> Excel([FromQuery] string searchText,
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
            ODataQueryOptions<DiscountStatisticsDto> options)
        {
            return _statisticsService.GetStatisticsAsync(options, searchText, startDate, endDate);
        }
    }
}