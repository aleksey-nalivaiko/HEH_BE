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
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public VendorService(IRepository<Vendor> vendorRepository,
            IDiscountRepository discountRepository, IMapper mapper)
            : base(vendorRepository, mapper)
        {
            _vendorRepository = vendorRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
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
            InitPhonesIds(vendor);
            InitAddressesIds(vendor);

            vendor.Id = vendor.Id == Guid.Empty ? Guid.NewGuid() : vendor.Id;

            InitVendorInfoInDiscounts(vendor);

            await _vendorRepository.CreateAsync(_mapper.Map<Vendor>(vendor));
            if (vendor.Discounts.Any())
            {
                await _discountRepository.CreateManyAsync(_mapper.Map<IEnumerable<Discount>>(vendor.Discounts));
            }
        }

        public async Task UpdateAsync(VendorDto vendor)
        {
            InitPhonesIds(vendor);
            InitAddressesIds(vendor);
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
        }

        public async Task RemoveAsync(Guid id)
        {
            await _vendorRepository.RemoveAsync(id);
            await _discountRepository.RemoveAsync(d => d.VendorId == id);
        }

        private VendorDto GetVendorDto(Vendor vendor, IEnumerable<Discount> vendorDiscounts)
        {
            var vendorDto = _mapper.Map<VendorDto>(vendor);
            vendorDto.Discounts = _mapper.Map<IEnumerable<DiscountDto>>(vendorDiscounts);
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

        private void InitAddressesIds(VendorDto vendor)
        {
            vendor.Addresses.ToList().ForEach(
                a => a.Id = a.Id == Guid.Empty ? Guid.NewGuid() : a.Id);
        }

        private void InitPhonesIds(VendorDto vendor)
        {
            vendor.Phones.ToList().ForEach(
                p => p.Id = p.Id == Guid.Empty ? Guid.NewGuid() : p.Id);
        }
    }
}