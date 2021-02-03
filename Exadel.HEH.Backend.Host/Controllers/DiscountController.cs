using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [ODataRoutePrefix("Discount")]
    //[ODataAuthorize]
    public class DiscountController : ODataController
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service;
        }

        [EnableQuery]
        [ODataRoute]
        public IQueryable<DiscountDto> Get([FromQuery] string searchText)
        {
            return _service.Get(searchText);
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        [ODataRoute("({id})")]
        public Task<DiscountDto> Get(Guid id)
        {
            return _service.GetByIdAsync(id);
        }
    }
}