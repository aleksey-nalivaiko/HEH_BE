using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [ODataRoutePrefix("Discount")]
    [ODataAuthorize]
    public class DiscountController : ODataController
    {
        private readonly IDiscountService _discountService;
        private readonly IDiscountValidationService _discountValidationService;

        public DiscountController(IDiscountService discountService,
            IDiscountValidationService discountValidationService)
        {
            _discountService = discountService;
            _discountValidationService = discountValidationService;
        }

        [EnableQuery]
        [ODataRoute]
        public Task<IQueryable<DiscountDto>> Get([FromQuery] string searchText)
        {
            return _discountService.GetAsync(searchText);
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        [ODataRoute("({id})")]
        public async Task<ActionResult<DiscountDto>> GetAsync(Guid id)
        {
            if (!await _discountValidationService.DiscountExists(id))
            {
                return NotFound();
            }

            return Ok(await _discountService.GetByIdAsync(id));
        }
    }
}