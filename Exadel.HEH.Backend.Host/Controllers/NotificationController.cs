using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
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

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{id:guid}")]
        public Task<NotificationDto> GetByIdAsync(Guid id)
        {
            return _notificationService.GetByIdAsync(id);
        }

        [HttpGet("count")]
        public Task<int> GetNotReadCountAsync()
        {
            return _notificationService.GetNotReadCountAsync();
        }
    }
}