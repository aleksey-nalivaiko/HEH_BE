using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class FavoritesController : BaseController<FavoritesDto>
    {
        private readonly IFavoritesService _favoritesService;
        private readonly IFavoritesValidationService _validationService;

        public FavoritesController(IFavoritesService favoritesService, IFavoritesValidationService validationService)
            : base(favoritesService)
        {
            _favoritesService = favoritesService;
            _validationService = validationService;
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