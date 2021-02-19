using System;
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
    [ODataRoutePrefix("Statistics")]
    [ODataAuthorize(Roles = nameof(UserRole.Administrator))]
    public class StatisticsController : ODataController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        [ODataRoute]
        public Task<IQueryable<DiscountStatisticsDto>> GetAsync([FromQuery] string searchText,
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return _statisticsService.GetStatisticsAsync(searchText, startDate, endDate);
        }
    }
}