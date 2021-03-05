using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(UserRole.Employee))]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        private readonly IVendorValidationService _vendorValidationService;

        public VendorController(IVendorService vendorService, IVendorValidationService vendorValidationService)
        {
            _vendorService = vendorService;
            _vendorValidationService = vendorValidationService;
        }

        /// <summary>
        /// Gets vendors. For users with employee role.
        /// </summary>
        [HttpGet]
        public Task<IEnumerable<VendorShortDto>> GetAllAsync()
        {
            return _vendorService.GetAllAsync();
        }

        /// <summary>
        /// Gets vendors from user location. For users with employee role.
        /// </summary>
        [HttpGet("location")]
        public Task<IEnumerable<VendorShortDto>> GetAllFromLocationAsync()
        {
            return _vendorService.GetAllFromLocationAsync();
        }

        /// <summary>
        /// Gets vendor by id. For users with moderator role.
        /// </summary>
        [HttpGet("{id:guid}")]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VendorDto>> GetByIdAsync(Guid id)
        {
            if (!await _vendorValidationService.VendorExistsAsync(id))
            {
                return NotFound();
            }

            return Ok(await _vendorService.GetByIdAsync(id));
        }

        /// <summary>
        /// Creates vendor. For users with moderator role.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync(VendorDto vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vendorService.CreateAsync(vendor);
            return Created(string.Empty, vendor);
        }

        /// <summary>
        /// Updates vendor. For users with moderator role.
        /// </summary>
        [HttpPut]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateAsync(VendorDto vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vendorService.UpdateAsync(vendor);
            return Ok(vendor);
        }

        /// <summary>
        /// Removes vendor. For users with moderator role.
        /// </summary>
        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveAsync(Guid id)
        {
            if (!await _vendorValidationService.VendorExistsAsync(id))
            {
                return NotFound();
            }

            await _vendorService.RemoveAsync(id);
            return Ok();
        }
    }
}