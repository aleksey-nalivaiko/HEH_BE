using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class VendorService : IVendorService
    {
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IMapper _mapper;

        public VendorService(IRepository<Vendor> vendorRepository, IMapper mapper)
        {
            _vendorRepository = vendorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendorDto>> GetAllVendors()
        {
            var vendorsTask = _vendorRepository.GetAllAsync();

            var vendors = await vendorsTask;

            var result = _mapper.Map<IEnumerable<VendorDto>>(vendors);

            return result;
        }
    }
}