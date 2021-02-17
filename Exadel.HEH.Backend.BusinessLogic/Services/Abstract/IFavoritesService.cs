using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IFavoritesService
    {
        Task<IQueryable<FavoritesDto>> GetAsync(string searchText = default);

        Task CreateAsync(FavoritesShortDto newFavorites);

        Task UpdateAsync(FavoritesShortDto newFavorites);

        Task RemoveAsync(Guid discountId);

        Task RemoveManyAsync(IEnumerable<Guid> discountIds);

        Task<Dictionary<Guid, bool>> DiscountsAreInFavorites(IEnumerable<Guid> discountsIds);

        Task<bool> DiscountIsInFavorites(Guid discountId);
    }
}