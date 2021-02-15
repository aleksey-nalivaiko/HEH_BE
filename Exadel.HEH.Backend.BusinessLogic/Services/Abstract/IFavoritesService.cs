using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IFavoritesService : IService<FavoritesDto>
    {
        Task CreateAsync(FavoritesCreateUpdateDto newFavorites);

        Task UpdateAsync(FavoritesCreateUpdateDto newFavorites);

        Task RemoveAsync(Guid discountId);

        Task RemoveManyAsync(IEnumerable<Guid> discountIds);

        Task<Dictionary<Guid, bool>> DiscountsAreInFavorites(IEnumerable<Guid> discountsIds);

        Task<bool> DiscountIsInFavorites(Guid discountId);
    }
}