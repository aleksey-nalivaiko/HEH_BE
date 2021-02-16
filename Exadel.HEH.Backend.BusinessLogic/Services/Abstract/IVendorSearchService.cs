using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IVendorSearchService
    {
        IQueryable<Discount> SearchVendors(IQueryable<Vendor> allVendors, string searchText);

        Task CreateAsync(VendorDto vendor);

        Task UpdateAsync(VendorDto vendor);

        Task RemoveAsync(Guid id);

        Task Reindex();
    }
}