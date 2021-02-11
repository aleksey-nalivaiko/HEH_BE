using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public FavoritesService(IUserRepository userRepository, IDiscountRepository discountRepository,
            IMapper mapper, IUserProvider userProvider)
        {
            _userRepository = userRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<IEnumerable<FavoritesDto>> GetAllAsync()
        {
            var user = await GetCurrentUser();
            var discounts = await _discountRepository.GetByIdsAsync(user.Favorites.Select(f => f.DiscountId));
            var discountsDto = _mapper.Map<IEnumerable<DiscountDto>>(discounts);
            var favoritesDto = _mapper.Map<IEnumerable<FavoritesDto>>(discountsDto).ToList();

            favoritesDto = favoritesDto.Join(
                user.Favorites,
                fd => fd.Id,
                f => f.DiscountId,
                (favDto, fav) =>
                {
                    favDto.Note = fav.Note;
                    return favDto;
                }).ToList();

            return favoritesDto;
        }

        public async Task CreateAsync(FavoritesCreateUpdateDto newFavorites)
        {
            var user = await GetCurrentUser();
            user.Favorites.Add(_mapper.Map<Favorites>(newFavorites));
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateAsync(FavoritesCreateUpdateDto newFavorites)
        {
            var user = await GetCurrentUser();
            var favorites = user.Favorites.FirstOrDefault(f => f.DiscountId == newFavorites.DiscountId);

            user.Favorites.Remove(favorites);
            user.Favorites.Add(_mapper.Map<Favorites>(newFavorites));
            await _userRepository.UpdateAsync(user);
        }

        public async Task RemoveAsync(Guid discountId)
        {
            var user = await GetCurrentUser();
            var favorites = user.Favorites.First(f => f.DiscountId == discountId);

            user.Favorites.Remove(favorites);
            await _userRepository.UpdateAsync(user);
        }

        public async Task<Dictionary<Guid, bool>> DiscountsAreInFavorites(IEnumerable<Guid> discountsIds)
        {
            var user = await GetCurrentUser();

            return discountsIds.GroupJoin(
                user.Favorites,
                d => d,
                f => f.DiscountId,
                (d, f) => new
                {
                    DiscountId = d,
                    IsFavorite = f.Any()
                }).ToDictionary(f => f.DiscountId, f => f.IsFavorite);
        }

        public async Task<bool> DiscountIsInFavorites(Guid discountId)
        {
            var user = await GetCurrentUser();

            return user.Favorites.FirstOrDefault(f => f.DiscountId == discountId) != null;
        }

        private Task<User> GetCurrentUser()
        {
            return _userRepository.GetByIdAsync(_userProvider.GetUserId());
        }
    }
}