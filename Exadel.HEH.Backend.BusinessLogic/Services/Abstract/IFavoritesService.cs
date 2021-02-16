using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IFavoritesService : IService<FavoritesDto>
    {
        Task CreateAsync(FavoritesShortDto newFavorites);

        Task UpdateAsync(FavoritesShortDto newFavorites);

        Task RemoveAsync(Guid discountId);

        Task RemoveManyAsync(IEnumerable<Guid> discountIds);

        Task<Dictionary<Guid, bool>> DiscountsAreInFavorites(IEnumerable<Guid> discountsIds);

        Task<bool> DiscountIsInFavorites(Guid discountId);
    }
}