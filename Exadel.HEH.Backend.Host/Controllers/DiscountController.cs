using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [ODataRoutePrefix("Discounts")]
    public class DiscountController : ODataController
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service;
        }

        [EnableQuery]
        [HttpGet]
        [ODataRoute]
        public IQueryable<DiscountDto> Get(ODataQueryOptions<DiscountDto> options)
        {
            return _service.GetAll();
        }
    }
}