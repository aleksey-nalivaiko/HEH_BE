using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class UserController : BaseController<UserDto>
    {
        private readonly IUserService _userService;
        private readonly IUserValidationService _validationService;

        public UserController(IUserService userService, IUserValidationService validationService)
            : base(userService)
        {
            _userService = userService;
            _validationService = validationService;
        }

        [Authorize(Roles = nameof(UserRole.Administrator))]
        public override Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return base.GetAllAsync();
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