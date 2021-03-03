﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
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

        [HttpGet("location")]
        public Task<IEnumerable<VendorShortDto>> GetAllFromLocationAsync()
        {
            return _vendorService.GetAllFromLocationAsync();
        }

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