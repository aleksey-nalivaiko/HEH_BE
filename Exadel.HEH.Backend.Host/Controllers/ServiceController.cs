using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
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

        [HttpPost("reindex")]
        public async Task ReindexAsync()
        {
            await _discountSearchService.ReindexAsync();
            await _vendorSearchService.ReindexAsync();
        }

        [HttpPost("initialize")]
        public async Task InitializeAsync()
        {
            await _identityService.InitializeAsync();
        }
    }
}