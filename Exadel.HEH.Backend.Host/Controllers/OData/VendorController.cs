using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Infrastructure;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers.OData
{
    [ODataRoutePrefix("Vendor")]
    [ODataAuthorize(Roles = nameof(UserRole.Moderator))]
    public class VendorController : ODataController
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        [ODataRoute]
        public IQueryable<VendorSearchDto> Get([FromQuery] string searchText,
            ODataQueryOptions<VendorSearchDto> options)
        {
            return _vendorService.Get(options, searchText);
        }
    }
}