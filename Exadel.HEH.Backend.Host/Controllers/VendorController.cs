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
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : BaseController<VendorShortDto>
    {
        private readonly IVendorService _vendorService;
        private readonly IVendorValidationService _vendorValidationService;

        public VendorController(IVendorService vendorService, IVendorValidationService vendorValidationService)
            : base(vendorService)
        {
            _vendorService = vendorService;
            _vendorValidationService = vendorValidationService;
        }

        [HttpGet("detailed")]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public Task<IEnumerable<VendorDto>> GetAllDetailedAsync()
        {
            return _vendorService.GetAllDetailedAsync();
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task<ActionResult<VendorDto>> GetByIdAsync(Guid id)
        {
            if (!await _vendorValidationService.VendorExists(id))
            {
                return NotFound();
            }

            return Ok(await _vendorService.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task<ActionResult> CreateAsync(VendorDto vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vendorService.CreateAsync(vendor);
            return Created(string.Empty, vendor);
        }

        [HttpPut]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task<ActionResult> UpdateAsync(VendorDto vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vendorService.UpdateAsync(vendor);
            return Ok(vendor);
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task<ActionResult> RemoveAsync(Guid id)
        {
            if (!await _vendorValidationService.VendorExists(id))
            {
                return NotFound();
            }

            await _vendorService.RemoveAsync(id);
            return Ok();
        }
    }
}