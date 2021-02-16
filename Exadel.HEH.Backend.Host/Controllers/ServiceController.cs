using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Administrator))]
    public class ServiceController : ControllerBase
    {
        private readonly ISearchService<Discount, DiscountDto> _discountSearchService;
        private readonly ISearchService<Vendor, VendorDto> _vendorSearchService;

        public ServiceController(ISearchService<Discount, DiscountDto> discountSearchService,
            ISearchService<Vendor, VendorDto> vendorSearchService)
        {
            _discountSearchService = discountSearchService;
            _vendorSearchService = vendorSearchService;
        }

        [HttpPost]
        public async Task ReindexAsync()
        {
            await _discountSearchService.ReindexAsync();
            await _vendorSearchService.ReindexAsync();
        }
    }
}
