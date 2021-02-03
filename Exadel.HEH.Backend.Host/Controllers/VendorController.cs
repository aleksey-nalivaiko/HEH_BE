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
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : BaseController<VendorShortDto>
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
            : base(vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet("detailed")]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public Task<IEnumerable<VendorDto>> GetAllDetailedAsync()
        {
            return _vendorService.GetAllDetailedAsync();
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public Task<VendorDto> GetByIdAsync(Guid id)
        {
            return _vendorService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task CreateAsync(VendorDto vendor)
        {
            await _vendorService.CreateAsync(vendor);
        }

        [HttpPut]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task UpdateAsync(VendorDto vendor)
        {
            await _vendorService.UpdateAsync(vendor);
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task RemoveAsync(Guid id)
        {
            await _vendorService.RemoveAsync(id);
        }
    }
}