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
    [ODataRoutePrefix("Vendor")]
    [ODataAuthorize(Roles = nameof(UserRole.Moderator))]
    public class VendorController : ODataController
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        /// <summary>
        /// Gets vendors. Filtering, sorting, pagination enabled via OData. For users with moderator role.
        /// </summary>
        /// <param name="searchText">
        /// For searching by name, discount conditions, categories, tags, countries, cities, streets.</param>
        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False,
            EnsureStableOrdering = false)]
        [ODataRoute]
        public Task<IQueryable<VendorSearchDto>> GetAsync([FromQuery] string searchText)
        {
            return _vendorService.GetAsync(searchText);
        }
    }
}