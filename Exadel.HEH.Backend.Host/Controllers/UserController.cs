using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class UserController : BaseController<User>
    {
        public UserController(IService<User> service)
            : base(service)
        {
        }
    }
}