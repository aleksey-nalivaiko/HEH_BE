using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
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
            var user = await _userRepository.GetByIdAsync(_userProvider.GetUserId());
            var favorites = new List<FavoritesDto>();

            foreach (var favoritesItem in user.Favorites)
            {
                var discount = await _discountRepository.GetByIdAsync(favoritesItem.DiscountId);
                var discountDto = _mapper.Map<DiscountDto>(discount);
                var favoritesDto = _mapper.Map<FavoritesDto>(discountDto);
                favoritesDto.Note = favoritesItem.Note;
                favorites.Add(favoritesDto);
            }

            return favorites;
        }

        public async Task CreateAsync(FavoritesDto newFavorites)
        {
            var user = await _userRepository.GetByIdAsync(_userProvider.GetUserId());
            user?.Favorites.Add(_mapper.Map<Favorites>(newFavorites));
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateAsync(FavoritesDto newFavorites)
        {
            var user = await _userRepository.GetByIdAsync(_userProvider.GetUserId());
            var favorites = user?.Favorites.FirstOrDefault(f => f.DiscountId == newFavorites.Id);
            if (favorites != null)
            {
                user.Favorites.Remove(favorites);
                user.Favorites.Add(_mapper.Map<Favorites>(newFavorites));
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task RemoveAsync(Guid discountId)
        {
            var user = await _userRepository.GetByIdAsync(_userProvider.GetUserId());
            var favorites = user?.Favorites.FirstOrDefault(f => f.DiscountId == discountId);
            if (favorites != null)
            {
                user.Favorites.Remove(favorites);
                await _userRepository.UpdateAsync(user);
            }
        }
    }
}