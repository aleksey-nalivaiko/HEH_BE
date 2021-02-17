using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ISearchService<Discount, Discount> _discountSearchService;
        private readonly ISearchService<VendorSearch, VendorDto> _vendorSearchService;

        public ServiceController(ISearchService<Discount, Discount> discountSearchService,
            ISearchService<VendorSearch, VendorDto> vendorSearchService)
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