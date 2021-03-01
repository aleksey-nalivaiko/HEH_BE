using System;
using System.IO;
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
        private const string FileName = "Statistics.xlsx";
        private const string FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        private readonly IStatisticsService _statisticsService;
        private readonly IExportService _exportService;

        public StatisticsController(IStatisticsService statisticsService, IExportService exportService)
        {
            _statisticsService = statisticsService;
            _exportService = exportService;
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

        [HttpGet]
        public async Task<FileResult> Excel([FromQuery] string searchText,
            [FromQuery] DateTime startDate, [FromQuery] DateTime endDate,
            ODataQueryOptions<DiscountStatisticsDto> options)
        {
            var stream = await _exportService.GetFileAsync(options, searchText, startDate, endDate);

            return File(stream, FileType, FileName);
        }
    }
}