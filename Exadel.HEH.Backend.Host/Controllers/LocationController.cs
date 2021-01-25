using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers.Abstract;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class LocationController : BaseController<LocationDto>
    {
        public LocationController(IService<LocationDto> service)
            : base(service)
        {
        }
    }
}
