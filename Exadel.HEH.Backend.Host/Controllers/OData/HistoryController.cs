using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;

namespace Exadel.HEH.Backend.Host.Controllers.OData
{
    [ODataRoutePrefix("History")]
    //[ODataAuthorize(Roles = nameof(UserRole.Administrator))]
    public class HistoryController : ODataController
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        [ODataRoute]
        public async Task<IEnumerable<HistoryDto>> Get(ODataQueryOptions<HistoryDto> options, [FromODataUri]int offset = 0)
        {
            return await _historyService.GetAllAsync(options);
        }
    }
}