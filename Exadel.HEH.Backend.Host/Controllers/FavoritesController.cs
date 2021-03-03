using System;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Employee))]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;
        private readonly IFavoritesValidationService _validationService;

        public FavoritesController(IFavoritesService favoritesService,
            IFavoritesValidationService validationService)
        {
            _favoritesService = favoritesService;
            _validationService = validationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveAsync(Guid discountId)
        {
            if (await _validationService.DiscountExists(discountId)
                && await _validationService.UserFavoritesNotExists(discountId))
            {
                await _favoritesService.RemoveAsync(discountId);
                return Ok();
            }

            return NotFound();
        }
    }
}