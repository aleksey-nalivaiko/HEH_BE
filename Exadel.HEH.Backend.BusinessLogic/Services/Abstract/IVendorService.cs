using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IVendorService : IService<VendorShortDto>
    {
        Task<IQueryable<VendorSearchDto>> GetAsync(string searchText = default);

        Task<IEnumerable<VendorShortDto>> GetAllFromLocationAsync();

        Task<VendorDto> GetByIdAsync(Guid id);

        Task CreateAsync(VendorDto vendorDto);

        Task UpdateAsync(VendorDto vendor);

        Task RemoveAsync(Guid id);
    }
}