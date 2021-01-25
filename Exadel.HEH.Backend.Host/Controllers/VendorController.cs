using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers.Abstract;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class VendorController : BaseController<VendorDto>
    {
        public VendorController(IService<VendorDto> service)
            : base(service)
        {
        }
    }
}
