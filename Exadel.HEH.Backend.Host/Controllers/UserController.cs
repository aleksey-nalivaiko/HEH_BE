using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class UserController : BaseController<UserDto>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
            : base(userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = nameof(UserRole.Administrator))]
        public override Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id:guid}")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _userService.GetByIdAsync(id));
            }

            return BadRequest();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("profile")]
        public Task<UserDto> GetProfile()
        {
            return _userService.GetProfile();
        }
    }
}