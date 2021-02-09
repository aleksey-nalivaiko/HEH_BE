using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IVendorService : IService<VendorShortDto>
    {
        Task<IEnumerable<VendorDto>> GetAllDetailedAsync();

        Task<VendorDto> GetByIdAsync(Guid id);

        Task CreateAsync(VendorDto vendorDto);

        Task UpdateAsync(VendorDto vendor);

        Task RemoveAsync(Guid id);
    }
}