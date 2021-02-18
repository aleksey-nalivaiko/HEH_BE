using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [ODataRoutePrefix("History")]
    [ODataAuthorize(Roles = nameof(UserRole.Administrator))]
    public class HistoryController : ODataController
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        [ODataRoute]
        public IQueryable<HistoryDto> Get()
        {
            return _historyService.Get();
        }
    }
}