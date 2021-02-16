using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers.Abstract;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class LocationController : BaseController<LocationDto>
    {
        public LocationController(ILocationService service)
            : base(service)
        {
        }
    }
}
