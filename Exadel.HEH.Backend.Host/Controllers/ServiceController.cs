using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ISearchService<Discount, Discount> _discountSearchService;
        private readonly IVendorSearchService _vendorSearchService;
        private readonly IIdentityService _identityService;

        public ServiceController(ISearchService<Discount, Discount> discountSearchService,
            IVendorSearchService vendorSearchService,
            IIdentityService identityService)
        {
            _discountSearchService = discountSearchService;
            _vendorSearchService = vendorSearchService;
            _identityService = identityService;
        }

        /// <summary>
        /// For reindexing search collections. BE only.
        /// </summary>
        [HttpPost("reindex")]
        public async Task ReindexAsync()
        {
            await _discountSearchService.ReindexAsync();
            await _vendorSearchService.ReindexAsync();
        }

        /// <summary>
        /// For initializing identity collections. BE only.
        /// </summary>
        [HttpPost("initialize")]
        public async Task InitializeAsync()
        {
            await _identityService.InitializeAsync();
        }
    }
}