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
    public class VendorService : BaseService<Vendor, VendorShortDto>, IVendorService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly IHistoryService _historyService;

        public VendorService(IVendorRepository vendorRepository,
            IDiscountService discountService,
            IMapper mapper,
            IHistoryService historyService)
            : base(vendorRepository, mapper)
        {
            _vendorRepository = vendorRepository;
            _discountService = discountService;
            _mapper = mapper;
            _historyService = historyService;
        }

        public async Task<IEnumerable<VendorDto>> GetAllDetailedAsync()
        {
            var discounts = await _discountService.GetAsync();
            var vendors = (await _vendorRepository.GetAllAsync()).ToList();

            var vendorsWithDiscounts = vendors.GroupJoin(
                discounts,
                vendor => vendor.Id,
                discount => discount.VendorId,
                GetVendorDto);

            return vendorsWithDiscounts;
        }

        public async Task<VendorDto> GetByIdAsync(Guid id)
        {
           var vendor = await _vendorRepository.GetByIdAsync(id);
           var vendorDiscounts = (await _discountService.GetAsync())
               .Where(d => d.VendorId == id);

           return GetVendorDto(vendor, vendorDiscounts);
        }

        public async Task CreateAsync(VendorDto vendor)
        {
            vendor.Id = vendor.Id == Guid.Empty ? Guid.NewGuid() : vendor.Id;

            await _vendorRepository.CreateAsync(_mapper.Map<Vendor>(vendor));

            await _historyService.CreateAsync(UserAction.Add,
                "Created vendor " + vendor.Id);

            if (AnyDiscounts(vendor.Discounts))
            {
                InitVendorInfoInDiscounts(vendor);
                await _discountService.CreateManyAsync(vendor.Discounts);
            }
        }

        public async Task UpdateAsync(VendorDto vendor)
        {
            await _vendorRepository.UpdateAsync(_mapper.Map<Vendor>(vendor));
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated vendor " + vendor.Id);

            var vendorDiscountsIds = new List<Guid>();

            if (AnyDiscounts(vendor.Discounts))
            {
                InitVendorInfoInDiscounts(vendor);
                RemoveIncorrectDiscountPhones(vendor);
                await _discountService.UpdateManyAsync(vendor.Discounts);

                vendorDiscountsIds = vendor.Discounts.Select(d => d.Id).ToList();
            }

            await _discountService.RemoveAsync(d =>
                d.VendorId == vendor.Id && !vendorDiscountsIds.Contains(d.Id));
        }

        public async Task RemoveAsync(Guid id)
        {
            await _vendorRepository.RemoveAsync(id);

            await _historyService.CreateAsync(UserAction.Remove,
                "Removed vendor " + id);

            await _discountService.RemoveAsync(d => d.VendorId == id);
        }

        private VendorDto GetVendorDto(Vendor vendor, IEnumerable<DiscountDto> vendorDiscounts)
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

        private void RemoveIncorrectDiscountPhones(VendorDto vendor)
        {
            var vendorPhonesIds = vendor.Phones == null ? new List<int>()
                : vendor.Phones.Select(p => p.Id).ToList();

            vendor.Discounts.ToList().ForEach(d =>
            {
                var phoneList = d.PhonesIds.ToList();
                phoneList.RemoveAll(p => !vendorPhonesIds.Contains(p));
                d.PhonesIds = phoneList;
            });
        }

        private bool AnyDiscounts(IEnumerable<DiscountDto> discounts)
        {
            return discounts != null && discounts.Any();
        }
    }
}