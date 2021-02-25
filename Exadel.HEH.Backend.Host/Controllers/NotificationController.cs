using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NotificationDto>> GetByIdAsync(Guid id)
        {
            if (await _notificationValidationService.NotificationExistsAsync(id))
            {
                return Ok(await _notificationService.GetByIdAsync(id));
            }

            return NotFound();
        }

        [HttpGet("count")]
        public Task<int> GetNotReadCountAsync()
        {
            return _notificationService.GetNotReadCountAsync();
        }
    }
}