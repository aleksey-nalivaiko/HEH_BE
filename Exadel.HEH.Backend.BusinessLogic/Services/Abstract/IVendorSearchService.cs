using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IVendorSearchService : ISearchService<VendorSearch, VendorDto>
    {
        Task<VendorSearch> GetByIdAsync(Guid id);
    }
}