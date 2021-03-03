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
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;
        private readonly IFavoritesValidationService _favoritesValidationService;
        private readonly IDiscountValidationService _discountValidationService;

        public FavoritesController(IFavoritesService favoritesService,
            IFavoritesValidationService favoritesValidationService,
            IDiscountValidationService discountValidationService)
        {
            _favoritesService = favoritesService;
            _favoritesValidationService = favoritesValidationService;
            _discountValidationService = discountValidationService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(FavoritesShortDto favorites)
        {
            if (ModelState.IsValid)
            {
                await _favoritesService.CreateAsync(favorites);
                return Created(string.Empty, favorites);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(FavoritesShortDto favorites)
        {
            if (ModelState.IsValid)
            {
                await _favoritesService.UpdateAsync(favorites);
                return Ok(favorites);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{discountId:guid}")]
        public async Task<ActionResult> RemoveAsync(Guid discountId)
        {
            if (await _discountValidationService.DiscountExists(discountId)
                && await _favoritesValidationService.UserFavoritesNotExists(discountId))
            {
                await _favoritesService.RemoveAsync(discountId);
                return Ok();
            }

            return NotFound();
        }
    }
}