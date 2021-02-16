using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IFavoritesService _favoritesService;
        private readonly IMapper _mapper;
        private readonly ISearchService<,> _searchService;
        private readonly IHistoryService _historyService;

        public DiscountService(IDiscountRepository discountRepository,
            IFavoritesService favoritesService,
            IVendorRepository vendorRepository,
            IMapper mapper,
            ISearchService<,> searchService,
            IHistoryService historyService)
        {
            _discountRepository = discountRepository;
            _favoritesService = favoritesService;
            _vendorRepository = vendorRepository;
            _mapper = mapper;
            _searchService = searchService;
            _historyService = historyService;
        }

        public async Task<IQueryable<DiscountDto>> GetAsync(string searchText)
        {
            var discounts = _discountRepository.Get();
            if (searchText != null)
            {
                discounts = _searchService.Search(discounts, searchText);
            }

            var discountsDto = discounts.ProjectTo<DiscountDto>(_mapper.ConfigurationProvider);
            var discountsDtoList = await discountsDto.ToListAsync();

            var areInFavorites = await _favoritesService.DiscountsAreInFavorites(discountsDtoList.Select(d => d.Id));

            discountsDto = discountsDtoList.Join(
                areInFavorites,
                d => d.Id,
                a => a.Key,
                (discount, a) =>
                {
                    discount.IsFavorite = a.Value;
                    return discount;
                }).AsQueryable();

            return discountsDto;
        }

        public async Task<DiscountExtendedDto> GetByIdAsync(Guid id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);
            var vendor = await _vendorRepository.GetByIdAsync(discount.VendorId);

            var discountDto = _mapper.Map<DiscountExtendedDto>(discount);

            discountDto.IsFavorite = await _favoritesService.DiscountIsInFavorites(discountDto.Id);
            discountDto.Links = _mapper.Map<IEnumerable<LinkDto>>(vendor.Links);
            discountDto.WorkingHours = vendor.WorkingHours;

            discountDto.Addresses = _mapper.Map<IEnumerable<AddressDto>>(vendor.Addresses.Join(
                discount.AddressesIds,
                a => a.Id,
                i => i,
                (a, i) => a));

            discountDto.Phones = _mapper.Map<IEnumerable<PhoneDto>>(vendor.Phones.Join(
                discount.PhonesIds,
                p => p.Id,
                i => i,
                (p, i) => p));

            return discountDto;
        }

        public async Task CreateManyAsync(IEnumerable<DiscountDto> discounts)
        {
            var discountsList = discounts.ToList();
            discountsList.ForEach(GenerateId);

            await _discountRepository.CreateManyAsync(
                _mapper.Map<IEnumerable<Discount>>(discounts));

            discountsList.ForEach(CreateHistoryAndSearchItems);
        }

        public async Task UpdateManyAsync(IEnumerable<DiscountDto> discounts)
        {
            var allDiscountsIds = _discountRepository.Get().Select(d => d.Id).ToList();
            var discountsList = discounts.ToList();

            discountsList.Where(d => !allDiscountsIds.Contains(d.Id))
                .ToList().ForEach(GenerateId);

            await _discountRepository.UpdateManyAsync(
                _mapper.Map<IEnumerable<Discount>>(discounts));

            foreach (var discount in discountsList)
            {
                if (allDiscountsIds.Contains(discount.Id))
                {
                    await _historyService.CreateAsync(UserAction.Edit,
                        "Updated discount " + discount.Id);

                    await _searchService.UpdateAsync(discount);
                }
                else
                {
                    CreateHistoryAndSearchItems(discount);
                }
            }
        }

        public async Task RemoveAsync(Expression<Func<Discount, bool>> expression)
        {
            var discountsToRemove = _discountRepository.Get().Where(expression).ToList();

            discountsToRemove.ForEach(d =>
            {
                _searchService.RemoveAsync(d.Id);

                _historyService.CreateAsync(UserAction.Remove,
                    "Removed discount " + d.Id);
            });

            await _discountRepository.RemoveAsync(expression);

            var discountsToRemoveIds = discountsToRemove.Select(d => d.Id);
            await _favoritesService.RemoveManyAsync(discountsToRemoveIds);
        }

        private void CreateHistoryAndSearchItems(DiscountDto discount)
        {
            _historyService.CreateAsync(UserAction.Add,
                "Created discount " + discount.Id);

            _searchService.CreateAsync(discount);
        }

        private void GenerateId(DiscountDto discount)
        {
            discount.Id = discount.Id == Guid.Empty ? Guid.NewGuid() : discount.Id;
        }
    }
}