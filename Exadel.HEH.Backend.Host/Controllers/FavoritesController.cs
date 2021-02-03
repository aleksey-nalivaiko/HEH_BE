using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : BaseController<FavoritesDto>
    {
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(IFavoritesService favoritesService)
            : base(favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(FavoritesDto favorites)
        {
            if (ModelState.IsValid)
            {
                await _favoritesService.CreateAsync(favorites);
                return Created(string.Empty, favorites);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public Task UpdateAsync(FavoritesDto favorites)
        {
            return _favoritesService.UpdateAsync(favorites);
        }

        [HttpDelete("{discountId:guid}")]
        public Task RemoveAsync(Guid discountId)
        {
            return _favoritesService.RemoveAsync(discountId);
        }
    }
}