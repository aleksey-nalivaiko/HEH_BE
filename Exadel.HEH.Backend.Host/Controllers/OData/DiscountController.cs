using System;
using System.Diagnostics.CodeAnalysis;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers.OData
{
    [ExcludeFromCodeCoverage]
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

        /// <summary>
        /// Gets discounts. Filtering, sorting, pagination enabled via OData. For users with employee role.
        /// </summary>
        /// <param name="searchText">
        /// For searching by conditions, vendor, category, tags, countries, cities, streets.</param>
        [EnableQuery(EnsureStableOrdering = false)]
        [ODataRoute]
        public Task<IQueryable<DiscountDto>> GetAsync([FromQuery] string searchText)
        {
            return _discountService.GetAsync(searchText);
        }

        /// <summary>
        /// Gets discount by id. For users with employee role.
        /// </summary>
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.None)]
        [ODataRoute("({id})")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DiscountExtendedDto>> GetAsync(Guid id)
        {
            if (!await _discountValidationService.DiscountExists(id))
            {
                return NotFound();
            }

            var discount = await _discountService.GetByIdAsync(id);

            await _statisticsService.IncrementViewsAmountAsync(id);

            return Ok(discount);
        }
    }
}