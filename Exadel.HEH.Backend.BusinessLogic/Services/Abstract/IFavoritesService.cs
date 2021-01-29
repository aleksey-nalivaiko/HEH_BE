using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IFavoritesService
    {
        Task<IEnumerable<FavoritesDto>> GetAllAsync();

        Task CreateAsync(FavoritesDto newFavorites);

        Task UpdateAsync(FavoritesDto newFavorites);

        Task RemoveAsync(Guid discountId);
    }
}