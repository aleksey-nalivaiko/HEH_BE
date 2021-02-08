using System;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface IVendorValidationService
    {
        Task<bool> VendorExists(Guid vendorId);
    }
}