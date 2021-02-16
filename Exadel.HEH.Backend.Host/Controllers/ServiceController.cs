using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public ServiceController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public Task Reindex()
        {
            return _searchService.Reindex();
        }
    }
}
