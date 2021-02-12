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
    public class VendorService : BaseService<Vendor, VendorShortDto>, IVendorService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly IHistoryService _historyService;

        public VendorService(IVendorRepository vendorRepository,
            IDiscountRepository discountRepository, IMapper mapper, IHistoryService historyService)
            : base(vendorRepository, mapper)
        {
            _vendorRepository = vendorRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
            _historyService = historyService;
        }

        public async Task<IEnumerable<VendorDto>> GetAllDetailedAsync()
        {
            var discounts = _discountRepository.Get().ToList();
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
           var vendorDiscounts = _discountRepository.Get().Where(d => d.VendorId == id).ToList();

           return GetVendorDto(vendor, vendorDiscounts);
        }

        public async Task CreateAsync(VendorDto vendor)
        {
            vendor.Id = vendor.Id == Guid.Empty ? Guid.NewGuid() : vendor.Id;

            InitVendorInfoInDiscounts(vendor);

            await _vendorRepository.CreateAsync(_mapper.Map<Vendor>(vendor));

            if (vendor.Discounts != null && vendor.Discounts.Any())
            {
                await _discountRepository.CreateManyAsync(_mapper.Map<IEnumerable<Discount>>(vendor.Discounts));

                foreach (var vendorDiscount in vendor.Discounts)
                {
                    await _historyService.CreateAsync(UserAction.Add,
                        "Created discount " + vendorDiscount.Id);
                }
            }

            await _historyService.CreateAsync(UserAction.Add,
                "Created vendor " + vendor.Id);
        }

        public async Task UpdateAsync(VendorDto vendor)
        {
            InitVendorInfoInDiscounts(vendor);

            await _vendorRepository.UpdateAsync(_mapper.Map<Vendor>(vendor));

            var discounts = _mapper.Map<IEnumerable<Discount>>(vendor.Discounts).ToList();

            var vendorDiscountsIds = discounts.Select(d => d.Id).ToList();
            var vendorPhonesIds = vendor.Phones.Select(p => p.Id).ToList();

            await _discountRepository.RemoveAsync(d => d.VendorId == vendor.Id
                                                       && !vendorDiscountsIds.Contains(d.Id));

            discounts.ForEach(d =>
            {
                var phoneList = d.PhonesIds.ToList();
                phoneList.RemoveAll(p => !vendorPhonesIds.Contains(p));
                d.PhonesIds = phoneList;
            });

            await _discountRepository.UpdateManyAsync(discounts);

            discounts.ForEach(x =>
            {
                    _historyService.CreateAsync(UserAction.Edit,
                        "Updated discount " + x.Id);
            });

            await _historyService.CreateAsync(UserAction.Edit,
                "Updated vendor " + vendor.Id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var discounts = await _discountRepository.Get().Where(x => x.VendorId == id).ToListAsync();

            await _vendorRepository.RemoveAsync(id);
            await _discountRepository.RemoveAsync(d => d.VendorId == id);

            discounts.ForEach(x =>
            {
                _historyService.CreateAsync(UserAction.Remove,
                    "Removed discount " + x.Id);
            });

            await _historyService.CreateAsync(UserAction.Remove,
                "Removed vendor " + id);
        }

        private VendorDto GetVendorDto(Vendor vendor, IEnumerable<Discount> vendorDiscounts)
        {
            var vendorDto = _mapper.Map<VendorDto>(vendor);
            vendorDto.Discounts = _mapper.Map<IEnumerable<DiscountDto>>(vendorDiscounts);
            return vendorDto;
        }

        private void InitVendorInfoInDiscounts(VendorDto vendor)
        {
            vendor.Discounts?.ToList().ForEach(d =>
            {
                d.VendorId = vendor.Id;
                d.VendorName = vendor.Name;
            });
        }
    }
}