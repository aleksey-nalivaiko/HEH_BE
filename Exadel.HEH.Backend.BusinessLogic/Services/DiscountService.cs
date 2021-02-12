using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IFavoritesService _favoritesService;
        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;
        private readonly ISearchService _searchService;

        public DiscountService(IDiscountRepository discountRepository,
            IFavoritesService favoritesService,
            IVendorService vendorService,
            IMapper mapper,
            ISearchService searchService)
        {
            _discountRepository = discountRepository;
            _favoritesService = favoritesService;
            _vendorService = vendorService;
            _mapper = mapper;
            _searchService = searchService;
        }

        public async Task<IQueryable<DiscountDto>> GetAsync(string searchText)
        {
            var discounts = _discountRepository.Get();
            if (searchText != null)
            {
                discounts = _searchService.SearchDiscounts(discounts, searchText);
            }

            var discountsDto = discounts.ProjectTo<DiscountDto>(_mapper.ConfigurationProvider);
            var discountsDtoList = discountsDto.ToList();

            var areInFavorites = await _favoritesService.DiscountsAreInFavorites(discountsDtoList.Select(d => d.Id));

            discountsDto = discountsDtoList.ToList().Join(
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
            var vendor = await _vendorService.GetByIdAsync(discount.VendorId);

            var discountDto = _mapper.Map<DiscountExtendedDto>(discount);

            discountDto.IsFavorite = await _favoritesService.DiscountIsInFavorites(discountDto.Id);
            discountDto.Links = vendor.Links;
            discountDto.WorkingHours = vendor.WorkingHours;

            discountDto.Addresses = vendor.Addresses.Join(
                discount.AddressesIds,
                a => a.Id,
                i => i,
                (a, i) => a);

            discountDto.Phones = vendor.Phones.Join(
                discount.PhonesIds,
                p => p.Id,
                i => i,
                (p, i) => p);

            return discountDto;
        }
    }
}