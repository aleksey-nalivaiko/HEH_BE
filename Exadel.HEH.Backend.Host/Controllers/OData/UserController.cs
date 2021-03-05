using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;

namespace Exadel.HEH.Backend.Host.Controllers.OData
{
    [ODataRoutePrefix("User")]
    [ODataAuthorize(Roles = nameof(UserRole.Administrator))]
    public class UserController : ODataController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets users. Filtering, sorting, pagination enabled via OData. For users with admin role.
        /// </summary>
        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False,
            EnsureStableOrdering = false)]
        [ODataRoute]
        public IQueryable<UserShortDto> Get()
        {
            return _userService.Get();
        }
    }
}