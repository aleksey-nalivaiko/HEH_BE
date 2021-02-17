using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Microsoft.AspNet.OData.Query;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IVendorService : IService<VendorShortDto>
    {
        IQueryable<VendorSearchDto> Get(ODataQueryOptions<VendorSearchDto> options, string searchText = default);

        Task<VendorDto> GetByIdAsync(Guid id);

        Task CreateAsync(VendorDto vendorDto);

        Task UpdateAsync(VendorDto vendor);

        Task RemoveAsync(Guid id);
    }
}