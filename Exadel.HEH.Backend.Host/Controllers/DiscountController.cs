using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [ODataRoutePrefix("Discount")]
    public class DiscountController : ODataController
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service;
        }

        [EnableQuery]
        [ODataRoute]
        public IQueryable<DiscountDto> Get()
        {
            return _service.Get();
        }
    }
}