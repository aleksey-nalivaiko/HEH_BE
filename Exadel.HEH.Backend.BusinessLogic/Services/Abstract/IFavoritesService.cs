using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IFavoritesService : IService<FavoritesDto>
    {
        Task CreateAsync(FavoritesDto newFavorites);

        Task UpdateAsync(FavoritesDto newFavorites);

        Task RemoveAsync(Guid discountId);
    }
}