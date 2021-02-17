using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;
        private readonly ISearchService<Discount, Discount> _searchService;

        public FavoritesService(IUserRepository userRepository,
            IMapper mapper,
            IUserProvider userProvider,
            ISearchService<Discount, Discount> searchService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userProvider = userProvider;
            _searchService = searchService;
        }

        public async Task<IQueryable<FavoritesDto>> GetAsync(string searchText = default)
        {
            var user = await GetCurrentUser();

            var searchedDiscounts = _searchService.Search(searchText);

            var favoritesIds = user.Favorites.Select(f => f.DiscountId);
            var discounts = searchedDiscounts.Where(d => favoritesIds.Contains(d.Id));

            var favoritesDto = discounts.ProjectTo<FavoritesDto>(_mapper.ConfigurationProvider);

            favoritesDto = favoritesDto.ToList().Join(
                user.Favorites,
                fd => fd.Id,
                f => f.DiscountId,
                (favDto, fav) =>
                {
                    favDto.Note = fav.Note;
                    return favDto;
                }).AsQueryable();

            return favoritesDto;
        }

        public async Task CreateAsync(FavoritesShortDto newFavorites)
        {
            var user = await GetCurrentUser();
            user.Favorites.Add(_mapper.Map<Favorites>(newFavorites));
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateAsync(FavoritesShortDto newFavorites)
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

        public async Task RemoveManyAsync(IEnumerable<Guid> discountIds)
        {
            var user = await GetCurrentUser();

            var favoritesList = user.Favorites.ToList();
            favoritesList.RemoveAll(f => discountIds.Contains(f.DiscountId));
            user.Favorites = favoritesList;

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