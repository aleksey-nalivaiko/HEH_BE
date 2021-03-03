using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string ImageName = "Photo.jpeg";
        private const int CacheAge = 24 * 60 * 60;

        private readonly IUserService _userService;
        private readonly IUserValidationService _validationService;

        public UserController(IUserService userService, IUserValidationService validationService)
        {
            _userService = userService;
            _validationService = validationService;
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            if (await _validationService.UserExists(id))
            {
                return Ok(await _userService.GetByIdAsync(id));
            }

            return NotFound();
        }

        [EnableCors("CorsForUI")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = CacheAge)]
        [HttpGet("photo/{id:guid}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPhotoByIdAsync(Guid id)
        {
            if (await _validationService.UserExists(id))
            {
                var image = await _userService.GetPhotoAsync(id);
                return File(image.Content, image.ContentType, ImageName);
            }

            return NotFound();
        }

        [EnableCors("CorsForUI")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = CacheAge, VaryByHeader = "Authorization")]
        [HttpGet("photo")]
        [Authorize(Roles = nameof(UserRole.Employee))]
        public async Task<IActionResult> GetPhotoAsync()
        {
            var image = await _userService.GetPhotoAsync();
            return File(image.Content, image.ContentType, ImageName);
        }

        [HttpGet("profile")]
        [Authorize(Roles = nameof(UserRole.Employee))]
        public Task<UserDto> GetProfileAsync()
        {
            return _userService.GetProfileAsync();
        }

        [HttpPut("profile")]
        [Authorize(Roles = nameof(UserRole.Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateNotificationsAsync(UserNotificationDto userNotifications)
        {
            if (ModelState.IsValid)
            {
                await _userService.UpdateNotificationsAsync(userNotifications);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id:guid}/{isActive:bool}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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