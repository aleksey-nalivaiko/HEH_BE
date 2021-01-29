using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(IFavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpGet]
        public Task<IEnumerable<FavoritesDto>> GetAllAsync()
        {
            return _favoritesService.GetAllAsync();
        }

        [HttpPost]
        public Task CreateAsync(FavoritesDto favorites)
        {
            return _favoritesService.CreateAsync(favorites);
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