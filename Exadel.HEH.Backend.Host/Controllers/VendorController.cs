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
        public Task CreateAsync(VendorDto vendor)
        {
            _vendorService.CreateAsync(vendor);
            return Task.CompletedTask;
        }

        [HttpPut]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public Task UpdateAsync(VendorDto vendor)
        {
            _vendorService.UpdateAsync(vendor);
            return Task.CompletedTask;
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public Task RemoveAsync(Guid id)
        {
            _vendorService.RemoveAsync(id);
            return Task.CompletedTask;
        }
    }
}