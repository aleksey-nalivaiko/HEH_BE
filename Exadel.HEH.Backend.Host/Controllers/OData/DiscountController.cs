using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers.OData
{
    [ODataRoutePrefix("Discount")]
    [ODataAuthorize(Roles = nameof(UserRole.Employee))]
    public class DiscountController : ODataController
    {
        private readonly IDiscountService _discountService;
        private readonly IDiscountValidationService _discountValidationService;
        private readonly IStatisticsService _statisticsService;

        public DiscountController(IDiscountService discountService,
            IDiscountValidationService discountValidationService,
            IStatisticsService statisticsService)
        {
            _discountService = discountService;
            _discountValidationService = discountValidationService;
            _statisticsService = statisticsService;
        }

        [EnableQuery]
        [ODataRoute]
        public Task<IQueryable<DiscountDto>> GetAsync([FromQuery] string searchText)
        {
            return _discountService.GetAsync(searchText);
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        [ODataRoute("({id})")]
        public async Task<ActionResult<DiscountExtendedDto>> GetAsync(Guid id)
        {
            if (!await _discountValidationService.DiscountExists(id))
            {
                return NotFound();
            }

            var discount = await _discountService.GetByIdAsync(id);

            await _statisticsService.IncrementViewsAmountAsync(discount.VendorId);

            return Ok(discount);
        }
    }
}