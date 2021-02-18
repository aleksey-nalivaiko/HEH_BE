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
using Microsoft.AspNet.OData.Query;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class VendorService : BaseService<Vendor, VendorShortDto>, IVendorService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly IHistoryService _historyService;
        private readonly ISearchService<VendorSearch, VendorDto> _searchService;

        public VendorService(IVendorRepository vendorRepository,
            IDiscountService discountService,
            IMapper mapper,
            IHistoryService historyService,
            ISearchService<VendorSearch, VendorDto> searchService)
            : base(vendorRepository, mapper)
        {
            _vendorRepository = vendorRepository;
            _discountService = discountService;
            _mapper = mapper;
            _historyService = historyService;
            _searchService = searchService;
        }

        public IQueryable<VendorSearchDto> Get(ODataQueryOptions<VendorSearchDto> options,
            string searchText)
        {
            var vendorsSearch = searchText != null ?
                _searchService.Search(searchText) : _searchService.Search();

            var vendorsSearchDto = vendorsSearch.ProjectTo<VendorSearchDto>(_mapper.ConfigurationProvider);

            if (options.Filter != null)
            {
                options.ApplyTo(vendorsSearchDto);
            }

            return vendorsSearchDto;
        }

        public async Task<VendorDto> GetByIdAsync(Guid id)
        {
           var vendor = await _vendorRepository.GetByIdAsync(id);
           var vendorDiscounts = (await _discountService.GetAllAsync())
               .Where(d => d.VendorId == id);

           return GetVendorDto(vendor, vendorDiscounts);
        }

        public async Task CreateAsync(VendorDto vendor)
        {
            vendor.Id = vendor.Id == Guid.Empty ? Guid.NewGuid() : vendor.Id;

            await _vendorRepository.CreateAsync(_mapper.Map<Vendor>(vendor));

            await _historyService.CreateAsync(UserAction.Add,
                "Created vendor " + vendor.Name);

            await _searchService.CreateAsync(vendor);

            if (AnyDiscounts(vendor.Discounts))
            {
                InitVendorInfoInDiscounts(vendor);
                var discounts = GetDiscountsFromDto(vendor);

                await _discountService.CreateManyAsync(discounts);
            }
        }

        public async Task UpdateAsync(VendorDto vendor)
        {
            await _vendorRepository.UpdateAsync(_mapper.Map<Vendor>(vendor));
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated vendor " + vendor.Name);

            await _searchService.UpdateAsync(vendor);

            var vendorDiscountsIds = new List<Guid>();

            var anyDiscounts = AnyDiscounts(vendor.Discounts);

            if (anyDiscounts)
            {
                vendorDiscountsIds = vendor.Discounts.Select(d => d.Id).ToList();
            }

            await _discountService.RemoveAsync(d =>
                d.VendorId == vendor.Id && !vendorDiscountsIds.Contains(d.Id));

            if (anyDiscounts)
            {
                InitVendorInfoInDiscounts(vendor);
                RemoveIncorrectDiscountPhones(vendor);

                var discounts = GetDiscountsFromDto(vendor);
                await _discountService.UpdateManyAsync(discounts);
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var vendor = await _vendorRepository.GetByIdAsync(id);

            await _vendorRepository.RemoveAsync(id);

            await _historyService.CreateAsync(UserAction.Remove,
                "Removed vendor " + vendor.Name);

            await _searchService.RemoveAsync(id);

            await _discountService.RemoveAsync(d => d.VendorId == id);
        }

        private VendorDto GetVendorDto(Vendor vendor, IEnumerable<DiscountShortDto> vendorDiscounts)
        {
            var vendorDto = _mapper.Map<VendorDto>(vendor);
            vendorDto.Discounts = vendorDiscounts;
            return vendorDto;
        }

        private void InitVendorInfoInDiscounts(VendorDto vendor)
        {
            vendor.Discounts.ToList().ForEach(d =>
            {
                d.VendorId = vendor.Id;
                d.VendorName = vendor.Name;
            });
        }

        private IEnumerable<Discount> GetDiscountsFromDto(VendorDto vendor)
        {
            var discounts = new List<Discount>();

            foreach (var vendorDiscount in vendor.Discounts)
            {
                var discount = _mapper.Map<Discount>(vendorDiscount);

                discount.Addresses = vendor.Addresses.Join(
                    vendorDiscount.AddressesIds,
                    a => a.Id,
                    i => i,
                    (a, i) => _mapper.Map<Address>(a)).ToList();

                discounts.Add(discount);
            }

            return discounts;
        }

        private void RemoveIncorrectDiscountPhones(VendorDto vendor)
        {
            var vendorPhonesIds = vendor.Phones == null ? new List<int>()
                : vendor.Phones.Select(p => p.Id);

            var discounts = vendor.Discounts.ToList();
            discounts.ForEach(d =>
            {
                var phoneList = d.PhonesIds.ToList();
                phoneList.RemoveAll(p => !vendorPhonesIds.Contains(p));
                d.PhonesIds = phoneList;
            });
        }

        private bool AnyDiscounts(IEnumerable<DiscountShortDto> discounts)
        {
            return discounts != null && discounts.Any();
        }
    }
}