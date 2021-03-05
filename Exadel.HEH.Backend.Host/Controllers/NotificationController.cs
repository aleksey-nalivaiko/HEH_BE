using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Employee))]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationValidationService _notificationValidationService;

        public NotificationController(INotificationService notificationService,
            INotificationValidationService notificationValidationService)
        {
            _notificationService = notificationService;
            _notificationValidationService = notificationValidationService;
        }

        /// <summary>
        /// Gets count of unread notifications. For users with employee role.
        /// </summary>
        [HttpGet("count")]
        public Task<int> GetNotReadCountAsync()
        {
            return _notificationService.GetNotReadCountAsync();
        }

        /// <summary>
        /// Reads notification. For users with employee role.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateIsReadAsync(Guid id)
        {
            if (!await _notificationValidationService.NotificationExistsAsync(id))
            {
                return NotFound();
            }

            await _notificationService.UpdateIsReadAsync(id);
            return Ok();
        }

        /// <summary>
        /// Reads all notifications. For users with employee role.
        /// </summary>
        [HttpPut]
        public Task UpdateAreReadAsync()
        {
            return _notificationService.UpdateAreReadAsync();
        }
    }
}