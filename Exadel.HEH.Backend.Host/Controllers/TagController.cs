using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers.Abstract;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class TagController : BaseController<TagDto>
    {
        public TagController(IService<TagDto> service)
            : base(service)
        {
        }
    }
}