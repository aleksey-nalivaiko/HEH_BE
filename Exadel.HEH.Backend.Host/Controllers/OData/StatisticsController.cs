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

        /// <summary>
        /// Gets statistics. Filtering, sorting, pagination enabled via OData. For users with admin role.
        /// </summary>
        /// <param name="searchText">
        /// For searching by conditions, vendor, category, tags, countries, cities, streets.</param>
        /// <param name="startDate">For retrieving statistics from specific date.</param>
        /// <param name="endDate">For retrieving statistics to specific date.</param>
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

        /// <summary>
        /// Exports statistics to Excel format file. Filtering, sorting, pagination enabled via OData. For users with admin role.
        /// </summary>
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