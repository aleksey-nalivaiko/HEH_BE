using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserProvider _userProvider;

        public FavoritesService(IUserRepository repository, IMapper mapper, IUserProvider userProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _userProvider = userProvider;
        }

        public async Task<IEnumerable<FavoritesDto>> GetAllAsync()
        {
            var user = await _repository.GetByIdAsync(_userProvider.GetUserId());
            return _mapper.Map<IEnumerable<FavoritesDto>>(user?.Favorites);
        }

        public async Task CreateAsync(FavoritesDto newFavorites)
        {
            var user = await _repository.GetByIdAsync(_userProvider.GetUserId());
            user?.Favorites.Add(_mapper.Map<Favorites>(newFavorites));
            await _repository.UpdateAsync(user);
        }

        public async Task UpdateAsync(FavoritesDto newFavorites)
        {
            var user = await _repository.GetByIdAsync(_userProvider.GetUserId());
            var favorites = user?.Favorites.FirstOrDefault(f => f.DiscountId == newFavorites.DiscountId);
            if (favorites != null)
            {
                user.Favorites.Remove(favorites);
                user.Favorites.Add(_mapper.Map<Favorites>(newFavorites));
                await _repository.UpdateAsync(user);
            }
        }

        public async Task RemoveAsync(Guid discountId)
        {
            var user = await _repository.GetByIdAsync(_userProvider.GetUserId());
            var favorites = user?.Favorites.FirstOrDefault(f => f.DiscountId == discountId);
            if (favorites != null)
            {
                user.Favorites.Remove(favorites);
                await _repository.UpdateAsync(user);
            }
        }
    }
}