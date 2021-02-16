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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserValidationService _validationService;

        public UserController(IUserService userService, IUserValidationService validationService)
        {
            _userService = userService;
            _validationService = validationService;
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            if (await _validationService.UserExists(id))
            {
                return Ok(await _userService.GetByIdAsync(id));
            }

            return NotFound();
        }

        [HttpGet("profile")]
        public Task<UserDto> GetProfileAsync()
        {
            return _userService.GetProfileAsync();
        }

        [HttpPut("profile")]
        public Task UpdateNotificationsAsync(NotificationDto notifications)
        {
            return _userService.UpdateNotificationsAsync(notifications);
        }

        [HttpPut("{id:guid}/{isActive:bool}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> UpdateStatusAsync(Guid id, bool isActive)
        {
            if (await _validationService.UserExists(id))
            {
                await _userService.UpdateStatusAsync(id, isActive);
                return Ok();
            }

            return NotFound();
        }

        [HttpPut("{id:guid}/{role}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> UpdateRoleAsync(Guid id, UserRole role)
        {
            if (await _validationService.UserExists(id))
            {
                await _userService.UpdateRoleAsync(id, role);
                return Ok();
            }

            return NotFound();
        }
    }
}