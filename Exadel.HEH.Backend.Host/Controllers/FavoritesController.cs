using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(FavoritesCreateUpdateDto favorites)
        {
            if (ModelState.IsValid)
            {
                await _favoritesService.CreateAsync(favorites);
                return Created(string.Empty, favorites);
            }

            return BadRequest(ModelState);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut]
        public async Task<ActionResult> UpdateAsync(FavoritesCreateUpdateDto favorites)
        {
            if (ModelState.IsValid)
            {
                await _favoritesService.UpdateAsync(favorites);
                return Ok(favorites);
            }

            return BadRequest(ModelState);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{discountId:guid}")]
        public async Task<ActionResult> RemoveAsync(Guid discountId)
        {
            if (await _validationService.ValidateDiscountIdIsExist(discountId)
                && await _validationService.ValidateUserFavoritesIsExist(discountId))
            {
                await _favoritesService.RemoveAsync(discountId);
                return Ok();
            }

            return NotFound();
        }
    }
}