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
    [ODataRoutePrefix("Notification")]
    [ODataAuthorize(Roles = nameof(UserRole.Employee))]
    public class NotificationController : ODataController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [EnableQuery(HandleNullPropagation = HandleNullPropagationOption.False)]
        [ODataRoute]
        public IQueryable<NotificationDto> GetAsync()
        {
            return _notificationService.Get();
        }
    }
}